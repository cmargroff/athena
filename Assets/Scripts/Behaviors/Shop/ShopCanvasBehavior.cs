using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public abstract class ShopCanvasBehavior : AthenaMonoBehavior
{
  public ShopBuildingBehavior.ShopTypeEnum ShopType;
  public GameObject ShopItemPrefab;
  public abstract void Show();
  public abstract void Hide();
  public abstract void Build();
}

public abstract class ShopCanvasBehavior<TAsset> : ShopCanvasBehavior where TAsset : BaseShopItemSO
{
  public List<TAsset> Items;
  public float ItemAnimationDuration = 1f;
  private Dictionary<string, (int, GameObject)> _shopItems;
  private Button _leaveButton;
  protected abstract string GetTitle();
  private @PlayerInputActions _controls;
  protected GameObject _shopItemsContainer;
  public override void Build()
  {
    _controls = new();
    _controls.Menues.Enable();
    _leaveButton = GetComponentInChildren<Button>();
    _shopItems = new();

    var title = transform.Find("ShopTitle").gameObject.GetComponent<TextMeshProUGUI>();
    if (title)
    {
      title.text = GetTitle();
    }

    _shopItemsContainer = transform.Find("ShopItems")?.gameObject;
    foreach (var item in Items)
    {
      var shopItem = Instantiate(ShopItemPrefab);
      shopItem.FindObjectByName("ItemTitle").GetComponent<TextMeshProUGUI>().text = item.FriendlyName;
      shopItem.FindObjectByName("ItemDescription").GetComponent<TextMeshProUGUI>().text = item.Description;
      var btn = shopItem.GetComponentInChildren<Button>();
      btn.onClick.AddListener(() => Buy(item));
      var img = shopItem.FindObjectByName("Icon").GetComponent<Image>();
      img.material = new Material(img.material); // make a copy of the material otherwise changing the values will update all items
      var mat = img.material;
      mat.color = item.Color;
      mat.SetTexture("_Icon", item.Icon);
      shopItem.transform.parent = _shopItemsContainer.transform;
      shopItem.transform.localRotation = Quaternion.identity;
      shopItem.transform.localPosition = Vector3.zero;
      shopItem.transform.localScale = Vector3.one;
      _shopItems[item.name] = (0, shopItem);
    }
    (_shopItemsContainer.transform as RectTransform).ArrangeChildrenAnchorsEvenly();
  }
  private void Buy(TAsset item)
  {
    var (count, shopItem) = _shopItems[item.name];
    count++;
    _shopItems[item.name] = (count, shopItem);
    // var btn = shopItem.GetComponentInChildren<Button>();
    if (count >= item.NumberInStore)
    {
      AnimateRemoveItem(item.name);
    }
  }
  public void AnimateRemoveItem(string name)
  {
    var seq = DOTween.Sequence();
    seq.Pause();

    // animate out the removed item
    seq.Insert(0f, _shopItems[name].Item2.GetComponent<CanvasGroup>().DOFade(0, ItemAnimationDuration));
    seq.Insert(0f, _shopItems[name].Item2.transform.DOScale(0, ItemAnimationDuration));

    // remove the item from the dictionary
    _shopItems.Remove(name);

    // animate the remaining items to fill the space
    // need to move the calculation of the new positions to the rect transform extension
    var increment = 1f / (_shopItems.Count+1);
    var i = 1;
    foreach (var item in _shopItems)
    {
      var (count, shopItem) = item.Value;
      var rect = shopItem.transform as RectTransform;
      seq.Insert(0f, DOTween.To(
          (float t) =>
          {
            rect.SetAnchor(new Vector2(t, 0.5f));
          },
          rect.anchorMin.x, increment * i,
          ItemAnimationDuration
        )
      );
      i++;
    }
    seq.Play();
  }
  public override void Show()
  {
    gameObject.SetActive(true);
    if (_shopItemsContainer)
    {
      // this code selects the first buy button in the shop but it doesnt resolve
      // the controller trying to take over after the mouse has interacted with the UI
      var item = _shopItemsContainer.transform.GetChild(0);
      if (item)
        item.gameObject.GetComponentInChildren<Button>().Select();
    }
  }
  public override void Hide()
  {
    gameObject.SetActive(false);
  }
  public void Leave()
  {
    Debug.Log("Leave");
    _gameManager.HideShop(ShopType);
  }
}
