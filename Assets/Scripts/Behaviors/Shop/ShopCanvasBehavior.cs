using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ShopCanvasBehavior<TAsset> : AthenaMonoBehavior where TAsset : BaseShopItemSO
{
  public GameObject ShopItemPrefab;
  public List<TAsset> Items;
  private GameObject _shopItemsContainer;
  private List<GameObject> _shopItems;
  protected abstract string GetTitle();
  protected override void Start()
  {
    base.Start();

    _shopItems = new List<GameObject>();

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
      var img = shopItem.FindObjectByName("Icon").GetComponent<Image>();
      img.material = new Material(img.material); // make a copy of the material otherwise changing the values will update all items
      var mat = img.material;
      mat.color = item.Color;
      mat.SetTexture("_Icon", item.Icon);
      shopItem.transform.parent = _shopItemsContainer.transform;
      shopItem.transform.localRotation = Quaternion.identity;
      shopItem.transform.localPosition = Vector3.zero;
      shopItem.transform.localScale = Vector3.one;
      _shopItems.Add(shopItem);
    }
    UpdateItemPositions();
  }
  private void UpdateItemPositions()
  {
    var increment = 1f / (Items.Count-1);
    var i = 0;
    foreach (var item in _shopItems)
    {
      var pos = new Vector2(increment * i, 0.5f);
      item.GetComponent<RectTransform>().SetAnchor(pos);
      i++;
    }
  }
  void Update()
  {

  }
}
