using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[RequireComponent(typeof(UIDocument))]
public class CollectedUIBehavior : AthenaMonoBehavior
{
    private VisualElement _pickupsContainer;
    public VisualTreeAsset PickupTemplate;
    private readonly Dictionary<string, VisualElement> _pickups = new();
    protected override void Start()
    {
        base.Start();
        var uiDocument = GetComponent<UIDocument>();
        uiDocument.useGUILayout = false;
        _pickupsContainer = uiDocument.rootVisualElement.Q("pickups");
        Debug.Log(_pickupsContainer);
        _gameManager.OnInventoryChanged += OnPickupCollected;
    }
    private void OnPickupCollected(string type, int count)
    {
        // this is not how you do this,
        // there is a way to map data into a visual tree
        if (!_pickups.ContainsKey(type))
        {
            var el = PickupTemplate.CloneTree();
            _pickups.Add(type, el);
            _pickupsContainer.Add(el);
        }
        _pickups.TryGetValue(type, out var pickup);
        var label = (Label)pickup.Q(className: "pickup-count");
        label.text = count.ToString();
    }
}