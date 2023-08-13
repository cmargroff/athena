using UnityEngine;

public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public int MinimumCost=5;

    private GameObject _indicator;

    protected override void Start()
    {
        base.Start();
        SetupIndicator();
    }

    public override void Interact()
    {
        base.Interact();
        _gameManager.Paused=true;
        switch (ShopType)
        {
            case ShopTypeEnum.Weapon:
                _gameManager.WeaponShop.gameObject.SetActive(true);
                _gameManager.WeaponShop.BuildShop();
                break;
            case ShopTypeEnum.Military:
                _gameManager.MilitaryShop.gameObject.SetActive(true);
                _gameManager.MilitaryShop.BuildShop();
                break;
            default:
                _gameManager.PowerUpShop.gameObject.SetActive(true);
                _gameManager.PowerUpShop.BuildShop();
                break;
        }
    }

    private void SetupIndicator()
    {
        _indicator = Instantiate(_indicatorTemplate, Vector3.zero, Quaternion.identity);
        _indicator.name = $"{name} warning";
        _indicator.transform.parent=transform;
        var behavior = _indicator.GetComponent<IndicatorBehavior>();
        behavior.Target = transform;

        //Warning.transform.parent = gameObject.transform;


        _indicator.gameObject.SetActive(true);
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup,
        Military
    }



}

