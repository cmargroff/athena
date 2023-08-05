using System;
using System.Collections.Generic;
using Assets.Scripts.UIViewModels;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public abstract class ShopBehavior<TAsset>:AthenaMonoBehavior where TAsset : BaseShopItemSO
{
    private UIDocument _shopUI;

    [SerializeField]
    private VisualTreeAsset _shopItemAsset;
    public List<TAsset> Items;

    public int GlobalMarkup;
    public int RepeatItemMarkup;

    private int _numberOfItemsSold;
    private readonly Dictionary<string, int> _itemsSold= new();
    private @PlayerInputActions _controls;
    protected abstract string GetTitle();

    //public override void  OnActive()
    //{
    //    base.OnActive();

    //    _shopUI = GetComponent<UIDocument>();
    //    SafeAssigned(_shopItemAsset);

    //}
    protected  override void Start()
    {
        _controls = new();
        _controls.Menues.Enable();
    }

    public void BuildShop()
    {
        _shopUI = GetComponent<UIDocument>();
        SafeAssigned(_shopItemAsset);
        var shopVM = new ShopVM()
        {
            Done = () =>
            {
                _gameManager.WeaponShop.gameObject.SetActive(false);
                _gameManager.Paused = false;
            },
            Coins = _gameManager.Pickups.GetValueOrDefault("Coin").ToString(),
            Title = GetTitle()

        };
        shopVM.Bind(_shopUI.rootVisualElement);

        var itemContainer = _shopUI.rootVisualElement.Q<ScrollView>("ItemsContainer");
        itemContainer.contentContainer.Clear();
        foreach (var item in Items)
        {
            var shopItem = _shopItemAsset.Instantiate();
            var cost = ComputeCost(item);
            var vm = new WeaponVM()
            {
                Name = item.FriendlyName,
                Description = item.Description,
                Cost = cost.ToString(),
                Buy = () => Buy(item),
                CanBuy = cost <= _gameManager.Pickups.GetValueOrDefault("Coin")
            };
            vm.Bind(shopItem);

            itemContainer.contentContainer.Add(shopItem);
        }
    }

    private int ComputeCost(TAsset asset)
    {
        var cost = asset.Cost;
        cost += _numberOfItemsSold * GlobalMarkup;
        cost+=_itemsSold.GetValueOrDefault(asset.name)* RepeatItemMarkup;
        return cost;
    }
   
    protected virtual void Buy(TAsset item)
    {
        _gameManager.Pickups["Coin"] -= ComputeCost(item);
        _numberOfItemsSold++;
        _itemsSold[item.name] = _itemsSold.GetValueOrDefault(item.name) + 1;
        if (item.NumberInStore <= _itemsSold[item.name])
        {
            Items.Remove(item);
        }


        BuildShop();
    }



}

