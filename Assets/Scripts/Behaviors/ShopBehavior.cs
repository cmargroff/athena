using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UIViewModels;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class ShopBehavior:AthenaMonoBehavior
{
    private UIDocument _shopUI;

    [SerializeField]
    private VisualTreeAsset _shopItemAsset;
    protected override void Start()
    {
        base.Start();

    }

    public List<WeaponSO> Weapons;


    public override void  OnActive()
    {
        base.OnActive();

        _shopUI = GetComponent<UIDocument>();
        SafeAssigned(_shopItemAsset);
        var shopVM = new ShopVM()
        {
            Done = () =>
            {
                _gameManager.Shop.gameObject.SetActive(false);
                _gameManager.Paused = false;
            },
            Coins = _gameManager.Pickups.GetValueOrDefault("Coin")
        };
        shopVM.Bind(_shopUI.rootVisualElement);

        var itemContainer = _shopUI.rootVisualElement.Q<ScrollView>("ItemsContainer");
        itemContainer.contentContainer.Clear();
        foreach (var weapon in Weapons)
        {
            var item = _shopItemAsset.Instantiate();
            var vm = new WeaponVM()
            {
                Name = weapon.FriendlyName,
                Description = weapon.Description,
                Cost=weapon.Cost,
                Buy = () => AddWeapon(weapon),
            };
            vm.Bind(item);

            itemContainer.contentContainer.Add(item);
        }
    }

    private void AddWeapon(WeaponSO weapon)
    {
        var weaponBehavior = _gameManager.Weapons.AddComponent<WeaponBehavior>();
        weaponBehavior.WeaponConfig=weapon;
        weaponBehavior.enabled = true;
    }



}

