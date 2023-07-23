using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[RequireComponent(typeof(UIDocument))]
public class MenuBehavior : AthenaMonoBehavior
{
  public List<string> MenuItems = new List<string>();
  public VisualTreeAsset MenuItemTemplate;

  protected override void Start()
  {
    base.Start();

    var root = GetComponent<UIDocument>().rootVisualElement;
    var menu = root.Q<VisualElement>("menu");
    foreach (var item in MenuItems)
    {
      var menuItem = MenuItemTemplate.CloneTree();
      var btn = menuItem.Q<Button>();
      btn.text = item;
      btn.clicked += () => Debug.Log($"Clicked {item}");
      menu.Add(menuItem);
    }
  }
}