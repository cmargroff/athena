using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public override void  OnActive()
    {
        base.OnActive();

        _shopUI = GetComponent<UIDocument>();
        SafeAssigned(_shopItemAsset);
        var doneButton = _shopUI.rootVisualElement.Q<Button>("DoneButton");
        doneButton.RegisterCallback<ClickEvent>(OnDoneButtonClick);
        doneButton.RegisterCallback<NavigationSubmitEvent>(OnDoneButtonNavigationSubmit);

        var itemContainer = _shopUI.rootVisualElement.Q<ScrollView>("ItemsContainer");
        itemContainer.contentContainer.Clear();
        for (int i = 1; i <= 3; i++)
        {
            var item = _shopItemAsset.Instantiate();
            var vm = new WeaponVM()
            {
                Name = "Test",
                Description = $"This is test number {i}",
                Cost=5*i,
                Buy = () => Debug.Log("Buy")
            };
            vm.Bind(item);

            itemContainer.contentContainer.Add(item);
        }


    }

    private void OnDoneButtonNavigationSubmit(NavigationSubmitEvent evt)
    {
        OnDone();
    }

    private void OnDoneButtonClick(ClickEvent evt)
    {
        OnDone();
    }

    private void OnDone()
    {
        _gameManager.Shop.gameObject.SetActive(false);
        _gameManager.Paused=false;
    }
}

