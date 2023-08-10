using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UIMeshes;

[assembly: UxmlNamespacePrefix("Athena.VisualElements", "ath")]
public class WeaponVE : VisualElement
{
  public static CustomStyleProperty<Texture> icon = new CustomStyleProperty<Texture>("--texture");
  public static readonly string ussClassName = "weapon";
  public static readonly string ussFillClassName = "weapon__fill";
  public static readonly string ussLabelClassName = "weapon__label";
  public static readonly string ussProgressClassName = "weapon__progress";
  public string friendlyName { get; set; }
  public Color color { get; set; }
  private float _progress;
  public float progress
  {
    get
    {
      return _progress;
    }
    set
    {
      _progress = value;
      MarkDirtyRepaint();
    }
  }
  private HexagonMesh _hexagonMesh;
  private Texture _icon;

  public new class UxmlFactory : UxmlFactory<WeaponVE, UxmlTraits> { }
  public new class UxmlTraits : VisualElement.UxmlTraits
  {
    UxmlStringAttributeDescription friendlyName = new UxmlStringAttributeDescription
    {
      name = "friendly-name",
      defaultValue = "weapon"
    };
    UxmlColorAttributeDescription color = new UxmlColorAttributeDescription
    {
      name = "color",
      defaultValue = new Color(0f, 0f, 0f, 1f)
    };
    UxmlFloatAttributeDescription progressAttr = new UxmlFloatAttributeDescription
    {
      name = "progress",
      defaultValue = 0
    };

    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
    {
      get
      {
        yield return new UxmlChildElementDescription(typeof(VisualElement));
      }
    }

    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
    {
      base.Init(ve, bag, cc);
      var wve = ve as WeaponVE;

      wve.friendlyName = friendlyName.GetValueFromBag(bag, cc);
      wve.color = color.GetValueFromBag(bag, cc);
      wve.progress = progressAttr.GetValueFromBag(bag, cc);
    }
  }
  public WeaponVE()
  {
    generateVisualContent += DrawMeshes;
    RegisterCallback<GeometryChangedEvent>(GeometryChangedCallback);
    RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
    AddToClassList(ussClassName);

    var fill = new VisualElement { name = "WeaponBackgroundFill" };
    fill.AddToClassList(ussFillClassName);
    Add(fill);

    var progress = new VisualElement { name = "WeaponProgressFill" };
    progress.AddToClassList(ussProgressClassName);
    Add(progress);

    var label = new Label { name = "WeaponLabel", text = friendlyName };
    label.AddToClassList(ussLabelClassName);
    Add(label);
  }
  private void DrawMeshes(MeshGenerationContext context)
  {
    _hexagonMesh.UpdateMesh();
    Debug.Log(_icon);
    var mesh = context.Allocate(_hexagonMesh.Vertices.Length, HexagonMesh.indices.Length, _icon);
    mesh.SetAllVertices(_hexagonMesh.Vertices);
    mesh.SetAllIndices(HexagonMesh.indices);
  }
  private void GeometryChangedCallback(GeometryChangedEvent evt)
  {
    UnregisterCallback<GeometryChangedEvent>(GeometryChangedCallback);
    Debug.Log("GeometryChangedCallback");
    _hexagonMesh = new HexagonMesh(this.worldBound.width / 2, color);
    MarkDirtyRepaint();
  }

  private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
  {
    if (evt.customStyle.TryGetValue(icon, out var textureValue))
    {
      Debug.Log("OnCustomStyleResolved");
      _icon = textureValue;
      MarkDirtyRepaint();
    }
  }
}