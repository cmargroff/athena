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
    public List<WeaponSO> Weapons;


    public override void  OnActive()
    {
        base.OnActive();

        _shopUI = GetComponent<UIDocument>();
        SafeAssigned(_shopItemAsset);
        BuildShop();
    }

    private void BuildShop()
    {
        var shopVM = new ShopVM()
        {
            Done = () =>
            {
                _gameManager.Shop.gameObject.SetActive(false);
                _gameManager.Paused = false;
            },
            Coins = _gameManager.Pickups.GetValueOrDefault("Coin").ToString()
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
                Cost = weapon.Cost.ToString(),
                Buy = () => BuyWeapon(weapon),
                CanBuy = weapon.Cost <= _gameManager.Pickups.GetValueOrDefault("Coin")
            };
            vm.Bind(item);

            itemContainer.contentContainer.Add(item);
        }
    }

    private void BuyWeapon(WeaponSO weapon)
    {
        _gameManager.Pickups["Coin"]-=weapon.Cost;
        BaseWeaponBehavior weaponBehavior;
        if (weapon.Behavior == WeaponSO.BehaviorEnum.Weapon)
        {
            weaponBehavior = _gameManager.Weapons.AddComponent<WeaponBehavior>();
        }
        else
        {
            weaponBehavior = _gameManager.Weapons.AddComponent<OrbitalBehavior>();
        }
        weaponBehavior.WeaponConfig=weapon;
        weaponBehavior.enabled = true;
        Weapons.Remove(weapon);
        BuildShop();
    }



}

