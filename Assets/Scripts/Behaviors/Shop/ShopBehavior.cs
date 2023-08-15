using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UIViewModels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public abstract class ShopBehavior:AthenaMonoBehavior
{
    public abstract void BuildShop();
    public int MinCost;
    public UnityEvent OnMinCostChanged;
}


[RequireComponent(typeof(UIDocument))]
public abstract class ShopBehavior<TAsset>: ShopBehavior where TAsset : BaseShopItemSO
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
        
        //gameObject.GetComponent<UIDocument>().enabled = false;//todo:this is all kind of dumb Make it less dumb
        //StartCoroutine(StartOff());

    }

    public override void DisabledStart()
    {
        base.DisabledStart();
        ComputeMinCost();
    }

    //private IEnumerator StartOff()
    //{

    //    //returning 0 will make it wait 1 frame
        
    //    yield return 0;
    //    gameObject.GetComponent<UIDocument>().enabled=true;
    //    gameObject.SetActive(false);

    //    //code goes here


    //}

    public void ComputeMinCost()
    {
        MinCost= Items.Min(ComputeCost);
        OnMinCostChanged?.Invoke();
    }


    public override void BuildShop()
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

    public int ComputeCost(TAsset asset)
    {
        var cost = asset.Cost;
        cost += _numberOfItemsSold * GlobalMarkup;
        cost+=_itemsSold.GetValueOrDefault(asset.name)* RepeatItemMarkup;
        return cost;
    }

    public virtual void Buy(TAsset item)
    {
        _gameManager.Pickups["Coin"] -= ComputeCost(item);
        _gameManager.OnCoinsChanged?.Invoke();

        _numberOfItemsSold++;
        _itemsSold[item.name] = _itemsSold.GetValueOrDefault(item.name) + 1;
        if (item.NumberInStore <= _itemsSold[item.name])
        {
            Items.Remove(item);
        }

        ComputeMinCost();
        BuildShop();
    }



}

