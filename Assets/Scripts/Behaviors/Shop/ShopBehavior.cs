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

    protected abstract string GetTitle();

    //public override void  OnActive()
    //{
    //    base.OnActive();

    //    _shopUI = GetComponent<UIDocument>();
    //    SafeAssigned(_shopItemAsset);
        
    //}

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
            var vm = new WeaponVM()
            {
                Name = item.FriendlyName,
                Description = item.Description,
                Cost = item.Cost.ToString(),
                Buy = () => Buy(item),
                CanBuy = item.Cost <= _gameManager.Pickups.GetValueOrDefault("Coin")
            };
            vm.Bind(shopItem);

            itemContainer.contentContainer.Add(shopItem);
        }
    }


    protected virtual void Buy(TAsset item)
    {
        _gameManager.Pickups["Coin"] -= item.Cost;
        Items.Remove(item);
        BuildShop();
    }



}

