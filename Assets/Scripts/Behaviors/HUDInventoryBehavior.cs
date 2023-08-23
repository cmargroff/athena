using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDInventoryBehavior : AthenaMonoBehavior
{
    public GameObject ItemPrefab;
    public PickupSO[] TrackedItems;
    private Dictionary<PickupTypeEnum, RectTransform> _items = new();

    private void Start()
    {
        var rect = GetComponent<RectTransform>();
        var i = 0;
        foreach (var item in TrackedItems)
        {
            var go = Instantiate(ItemPrefab, rect);
            var itemRect = go.GetComponent<RectTransform>();
            _items.Add(item.Type, itemRect);
            itemRect.Bind(new { Count = 0, Color = item.Color });
            Debug.Log(itemRect.rect.width);
            itemRect.anchoredPosition = new Vector2(i * -1 * itemRect.rect.width, 0);
            i++;
        }

        _gameManager.OnInventoryChanged += InventoryChanged;
    }

    private void InventoryChanged(PickupTypeEnum type, int count)
    {
        if (!_items.ContainsKey(type))
        {
            return;
        }
        _items[type].Bind(new { Count = count });
    }
}