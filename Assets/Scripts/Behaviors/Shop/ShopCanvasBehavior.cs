using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ShopCanvasBehavior : AthenaMonoBehavior
{
    public int GlobalMarkup;
    public int RepeatItemMarkup;
    public ShopBuildingBehavior.ShopTypeEnum ShopType;
    public GameObject ShopItemPrefab;
    public abstract void Show();
    public abstract void Hide();
    public abstract void Build();
    public UnityEvent<int> OnMinCostChanged;

    public float Damage;
    public float Weight;
    public float Speed;
    public float Health;
    public float SpawnFrequency;

    protected void MinCostChanged(int min)
    {
        OnMinCostChanged?.Invoke(min);
    }
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
    private int _numberOfItemsSold;

    public override void Build()
    {
        _controls = new();
        _shopItems = new();

        var rect = GetComponent<RectTransform>();
        rect.Bind(new { ShopTitle = GetTitle() });

        _shopItemsContainer = transform.Find("ShopItems").gameObject;
        _leaveButton= transform.Find("LeaveButton").GetComponent<Button>();
        if (_shopItemsContainer == null)
        {
            //Debug.LogError("ShopItems container not found");
            return;
        }

        foreach (var item in Items)
        {
            var shopItem = Instantiate(ShopItemPrefab);
            var itemRect = shopItem.GetComponent<RectTransform>();
            //  need to make copy of material to set individual properties per item
            var img = shopItem.FindObjectByName("Icon").GetComponent<Image>();
            img.material = new Material(img.material);

            itemRect.Bind(new
            {
                ItemTitle = item.FriendlyName,
                ItemDescription = item.Description,
                Icon = item.Icon,
                Color = item.Color,
                Cost = item.Cost.ToString(),
                Buy = (Action)(() => Buy(item))
            });
            shopItem.transform.SetParent(_shopItemsContainer.transform, false);
            shopItem.transform.ResetLocal();
            _shopItems[item.name] = (0, shopItem);
        }
        


        UpdateBinds();

        (_shopItemsContainer.transform as RectTransform).ArrangeChildrenAnchorsEvenly();
    }

    public override void DisabledStart()
    {
        base.DisabledStart();
        _gameManager.StartCoroutine(DisabledStartWorker()); //has to be done through game manager to prevent inactive issue
    }

    private IEnumerator DisabledStartWorker()
    {
        yield return 0;
        UpdateMinCost();
    }


    private void UpdateBinds()
    {
        var currentCoins = _gameManager.Pickups.GetValueOrDefault(PickupTypeEnum.Coin);
        foreach (var item in _shopItems)
        {
            var (count, shopItem) = item.Value;
            var so = Items.Find(i => i.name == item.Key);
            var cost = ComputeCost(count, so);
            var itemRect = shopItem.GetComponent<RectTransform>();
            itemRect.Bind(new
            {
                Cost = ComputeCost(count, so).ToString(),
                CanBuy = cost <= currentCoins
            });
        }
    }
    public override void OnActive()
    {
        base.OnActive();
        UpdateBinds();
       
    }
    protected void Spend(TAsset item)
    {
        

        var (count, shopItem) = _shopItems[item.name];
        var cost = ComputeCost(count, item);
        _gameManager.Pickups[PickupTypeEnum.Coin] -= cost;
        _gameManager.OnInventoryChanged?.Invoke(PickupTypeEnum.Coin);
        _numberOfItemsSold++;
        count++;
        _shopItems[item.name] = (count, shopItem);
        // var btn = shopItem.GetComponentInChildren<Button>();
        if (count >= item.NumberInStore)
        {
            AnimateRemoveItem(item.name);
        }
        else
        {
            AnimateBuyItem(item.name);
        }
        UpdateBinds();
        UpdateMinCost();
        _gameManager.SetItemAsSelected(_leaveButton.gameObject);
    }
    public virtual void Buy(TAsset item)
    {
        Spend(item);
        UpdateEnemyCharacter();
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
        var increment = 1f / (_shopItems.Count - 1);
        if (_shopItems.Count == 1)
        {
            increment = 0f;
        }
        var i = 0;
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
        var containerRect = _shopItemsContainer.transform as RectTransform;
        var scale = (1f - (1f / (_shopItems.Count + 1f)));
        var newWidth = containerRect.rect.width * scale;
        if (_shopItems.Count == 1)
        {
            newWidth = 0f;
        }
        seq.Insert(0f, DOTween.To((float w) =>
        {
            containerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        }, containerRect.rect.width, newWidth, ItemAnimationDuration));
        seq.Play();
    }

    private void AnimateBuyItem(string name)
    {
        var (count, item) = _shopItems[name];
        var seq = DOTween.Sequence();
        seq.Pause();
        seq.Append(item.transform.DOScale(0.7f, 0.1f).SetEase(Ease.OutSine));
        seq.Append(item.transform.DOScale(1f, 0.05f).SetEase(Ease.InSine));
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
        _gameManager.SetItemAsSelected(_leaveButton.gameObject);
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
    private int ComputeCost(int sold, TAsset item)
    {
        var cost = item.Cost;
        cost += _numberOfItemsSold * GlobalMarkup;
        cost += sold * RepeatItemMarkup;
        return cost;
    }
    private void UpdateMinCost()
    {
        var min = int.MaxValue;
        foreach (var item in _shopItems)
        {
            var (count, shopItem) = item.Value;
            var so = Items.Find(i => i.name == item.Key);
            var cost = ComputeCost(count, so);
            if (cost < min)
            {
                min = cost;
            }
        }
        MinCostChanged(min);
    }
    protected void UpdateEnemyCharacter()
    {
        _gameManager.EnemyCharacter.Damage += Damage;
        _gameManager.EnemyCharacter.Weight += Weight;
        _gameManager.EnemyCharacter.Speed += Speed;
        _gameManager.EnemyCharacter.Health += Health;
        _gameManager.EnemyCharacter.SpawnFrequency += SpawnFrequency;
    }
}