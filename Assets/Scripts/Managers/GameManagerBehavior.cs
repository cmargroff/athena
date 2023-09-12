using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    public uint FrameCount = 1;
    public float KnockbackFriction = 0.1f;
    public float KnockbackFactor = 1f;
    public Dictionary<PickupTypeEnum, int> Pickups = new();
    public PlayerCharacterBehavior PlayerCharacter;
    public BuildingCharacterBehavior BuildingCharacter;
    public EnemyCharacterBehaviour EnemyCharacter;
    public event Action<PickupTypeEnum, int> OnInventoryChanged;
    public event Action<float> PlayerHealthChanged;
    //debug events
    public UnityEvent<VulnerableBehavior, float> OnEnemyHealthChanged;
    //end debug events
    public CameraBehavior CameraBehavior;

    private ApplicationManager _applicationManager;
    private @PlayerInputActions _controls;

    public AudioSource AudioSource;
    public CaptionBehavior Caption;

    protected override void Awake()
    {
        base.Awake();
        PlayerCharacter = GetComponent<PlayerCharacterBehavior>();
        BuildingCharacter = GetComponent<BuildingCharacterBehavior>();
        EnemyCharacter = GetComponent<EnemyCharacterBehaviour>();
        _applicationManager = SafeFindObjectOfType<ApplicationManager>();
    }
    protected override void Start()
    {
        base.Start();
        DOTween.SetTweensCapacity(10000, 10000);

        SafeAssigned(Bounds);
        SafeAssigned(Player);
        SafeAssigned(Weapons);
        SafeAssigned(CameraBehavior);
        SafeAssigned(AudioSource);
        SafeAssigned(Caption);
        CreateShops();
        RunDisabledStarts();

        _controls = new();
        _controls.Game.Enable();

        _controls.Game.Pause.performed += context => { Pause(); };

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
                obj.transform.SetParent(ShopCanvas.transform, false);
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
        te.SetFramesInSeconds(seconds, FrameCount);
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
            if (kv.Value.IsActiveFrame(FrameCount))
            {
                kv.Value.Action();
            }
        }
        FrameCount++;
    }
    public void UpdatePlayerHealth(float health)
    {
        PlayerHealthChanged?.Invoke(health);
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
        if (Pickups.ContainsKey(pickup.Type))
        {
            Pickups[pickup.Type] += pickup.Amount;
        }
        else
        {
            Pickups.Add(pickup.Type, pickup.Amount);
        }
        OnInventoryChanged?.Invoke(pickup.Type, Pickups[pickup.Type]);

        pickup.SpecialAction?.Invoke();
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

    public void EndLevel()
    {
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }
    public void Lose()
    {
        _applicationManager.EndGameInLoss();
    }

    public void Pause()
    {
        Paused = true;
        _applicationManager.OpenSettings(UnPause);

    }

    public void UnPause()
    {
        Paused=false;
    }
    public void PlayVoiceClip(VoiceLine line) {
        AudioSource.Stop();
        AudioSource.PlayOneShot(line.VoiceClip);
        Caption.SetCaption(line);
    }

}