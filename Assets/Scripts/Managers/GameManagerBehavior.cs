using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCharacterBehavior))]
public class GameManagerBehavior : AthenaMonoBehavior
{
    public bool Paused;
    public Collider2D Bounds;
    public GameObject Player;
    public GameObject Weapons;
    public PoolBehavior Pool;
    public LayerMask Enemies;
    public LayerMask Buildings;
    public List<GameObject> Shops;
    private Dictionary<ShopBuildingBehavior.ShopTypeEnum, ShopCanvasBehavior> _shops = new();
    private readonly Dictionary<Guid, TimedEvent> _timedEvents = new();
    private Int64 _frameCount = 1;
    public float KnockbackFriction = 0.1f;
    public float KnockbackFactor = 1f;
    public Dictionary<string, int> Pickups = new();
    public PlayerCharacterBehavior PlayerCharacter;
    public BuildingCharacterBehavior BuildingCharacter;
    public EnemyCharacterBehaviour EnemyCharacter;
    public event Action<string, int> OnInventoryChanged;
    //debug events
    public UnityEvent<VulnerableBehavior> OnEnemyChanged;
    public UnityEvent<float> OnEnemyDamaged;
    //end debug events
    public CameraBehavior CameraBehavior;

    protected override void Awake()
    {
        base.Awake();
        PlayerCharacter = GetComponent<PlayerCharacterBehavior>();
        BuildingCharacter = GetComponent<BuildingCharacterBehavior>();
        EnemyCharacter = GetComponent<EnemyCharacterBehaviour>();
    }
    protected override void Start()
    {
        base.Start();
        DOTween.SetTweensCapacity(10000, 10000);

        SafeAssigned(Bounds);
        SafeAssigned(Player);
        SafeAssigned(Weapons);
        SafeAssigned(CameraBehavior);
        CreateShops();
        RunDisabledStarts();
    }
    private void CreateShops()
    {
        var ShopCanvas = GameObject.Find("ShopCanvas");
        foreach (var shop in Shops)
        {
            var shopBehavior = shop.GetComponent<ShopCanvasBehavior>();
            Debug.Log(shopBehavior.ShopType);
            if (shopBehavior)
            {
                var obj = Instantiate(shop);
                obj.SetActive(false);
                obj.transform.parent = ShopCanvas.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.transform.localRotation = Quaternion.identity;
                var b = obj.GetComponent<ShopCanvasBehavior>();
                b.Build();

                _shops.Add(shopBehavior.ShopType, b);
                //b.DisabledStart();//run this manually because it's added too late
            }
        }
    }
    public ShopCanvasBehavior GetShop(ShopBuildingBehavior.ShopTypeEnum shopType)
    {
        _shops.TryGetValue(shopType, out var shop);
        return shop;
    }
    public void ShowShop(ShopBuildingBehavior.ShopTypeEnum shopType)
    {
        Paused = true;
        _shops.TryGetValue(shopType, out var shop);
        if (shop)
        {
            shop.Show();
        }
    }
    public void HideShop(ShopBuildingBehavior.ShopTypeEnum shopType)
    {
        Paused = false;
        _shops.TryGetValue(shopType, out var shop);
        if (shop)
        {
            shop.Hide();
        }
    }
    public TimedEvent AddTimedEvent(float seconds, Action action, GameObject owner)
    {
        if (seconds == 0)
        {
            throw new ArgumentException("Setting things to repeat every zero seconds is not right");
        }

        var te = new TimedEvent()
        {
            Id = Guid.NewGuid(),
            Action = action,
            Owner = owner
        };
        te.SetFramesInSeconds(seconds);
        _timedEvents.Add(te.Id, te);
        return te;
    }
    public void RemoveTimedEvent(TimedEvent te)
    {
        _timedEvents.Remove(te.Id);
    }

    protected override void PlausibleFixedUpdate()
    {
        foreach (var kv in _timedEvents)
        {
            if (kv.Value.Owner.activeInHierarchy)
            {
                if (_frameCount % kv.Value.Frames == 0)
                {
                    kv.Value.Action();
                }
            }
        }
        _frameCount++;
    }
    //public void UseInvtentoryItem(string name, int amount)
    //{
    //    if (Pickups.ContainsKey(name))
    //    {
    //        Pickups[name] -= amount;
    //        OnInventoryChanged?.Invoke(name, Pickups[name]);
    //    }
    //}
    public void CollectPickup(PickupBehavior pickup)
    {
        if (Pickups.ContainsKey(pickup.Name))
        {
            Pickups[pickup.Name] += pickup.Amount;
        }
        else
        {
            Pickups.Add(pickup.Name, pickup.Amount);
        }
        OnInventoryChanged?.Invoke(pickup.Name, Pickups[pickup.Name]);
    }
    private void RunDisabledStarts()
    {
        var objects = FindObjectsOfType<AthenaMonoBehavior>(true);
        foreach (var obj in objects)
        {
            if (obj.gameObject.activeInHierarchy == false)
            {
                obj.DisabledStart();
            }
        }
    }
}