using UnityEngine;
[RequireComponent(typeof(BuildingHoverBehaviour))]
public class BuildingInteractBehaviour : AthenaMonoBehavior
{
    [SerializeField]
    protected GameObject _indicatorTemplate;
    [HideInInspector]
    public BuildingHoverBehaviour BuildingHover;
    protected override void Start()
    {
        BuildingHover = GetComponent<BuildingHoverBehaviour>();
    }
    public virtual void Interact()
    {
        Debug.Log("Building interaction");
    }
}