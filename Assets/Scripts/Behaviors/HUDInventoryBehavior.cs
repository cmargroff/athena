using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryBehavior : AthenaMonoBehavior
{
    public GameObject ItemPrefab;
    private Dictionary<PickupTypeEnum, RectTransform> _items = new();

    private void Start()
    {

        _gameManager.OnInventoryChanged += InventoryChanged;
    }

    private void InventoryChanged(PickupTypeEnum type, int count)
    {

    }
}