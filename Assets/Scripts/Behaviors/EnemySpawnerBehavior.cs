using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;

public class EnemySpawnerBehavior : AthenaMonoBehavior
{
    [SerializeField]
    private BoxCollider2D _spawnBoundry;
    public LevelSO Level;

    //private Vector3 _trueBoundingScale=Vector3.one;

    private Sequence _sequence;

    protected override void Start()
    {
        base.Start();

        //var obj = _spawnBoundry.transform;
        //do
        //{
        //    _trueBoundingScale = new  Vector3(_trueBoundingScale.x * obj.localScale.x, _trueBoundingScale.y * obj.localScale.y, _trueBoundingScale.z * obj.localScale.z);
        //    obj = obj.parent;
        //} while (obj.parent != null);

        SafeAssigned(_spawnBoundry);


        BuildSpawnTriggers();
    }

    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _sequence.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }


    private void BuildSpawnTriggers()
    {
        int startPos = 0;
        int endPos = 0;

        float timelineTime=0f;
        _sequence = DOTween.Sequence();
        _sequence.SetUpdate(UpdateType.Manual);
        while (startPos < Level.EnemyTimings.Count || endPos < Level.EnemyTimings.Count)
        {
            if ((Level.EnemyTimings.GetValueOrDefault(startPos)?.StartTime??float.PositiveInfinity) <= (Level.EnemyTimings.GetValueOrDefault(endPos)?.StartTime ?? float.PositiveInfinity))
            {
                _sequence.AppendInterval(Level.EnemyTimings[startPos].StartTime - timelineTime);
                var pos = startPos;
                _sequence.AppendCallback(() =>
                {
                    Debug.Log($"starting {Level.EnemyTimings[pos].Name} at {Level.EnemyTimings[pos].StartTime} game time {Time.realtimeSinceStartup }");
                    Level.EnemyTimings[pos].Timer = _gameManager.AddTimedEvent(Level.EnemyTimings[pos].Rate, () =>
                    {
                        SpawnEnemy(Level.EnemyTimings[pos].Enemy, Level.EnemyTimings[pos].Aggressiveness, Level.EnemyTimings[pos].Sides);
                      
                    }, gameObject);
                });



                timelineTime = Level.EnemyTimings[startPos].StartTime;
                startPos++;
            }
            else
            {
                _sequence.AppendInterval(Level.EnemyTimings[endPos].EndTime - timelineTime);
                var pos = endPos;
                _sequence.AppendCallback(() =>
                {
                    Debug.Log($"stopping {Level.EnemyTimings[pos].Name} at {Level.EnemyTimings[pos].EndTime} game time {Time.realtimeSinceStartup }");
                    _gameManager.RemoveTimedEvent(Level.EnemyTimings[pos].Timer);
                });
                timelineTime = Level.EnemyTimings[endPos].EndTime;
                endPos++;
            }
        }
    }





    private void SpawnEnemy(EnemySO enemySO, float aggressiveness, EnemyTiming.SidesEnum side)
    {
        var newPosition = GetRandomPointOnBorder(_spawnBoundry, aggressiveness, side);
        var enemy = _gameManager.Pool.GetPooledObject(enemySO.Prefab, newPosition, Quaternion.identity);
        var flying = enemy.GetComponent<FlyingBehavior>();
        var damaging = enemy.GetComponent<DamagingBehavior>();
        var vulnerable = enemy.GetComponent<VulnerableBehavior>();
        var rewardDrop = enemy.GetComponent<RewardDropBehavior>();

        flying.Speed = enemySO.Speed;
        damaging.Damage = enemySO.TouchDamage;
        vulnerable.MaxHealth = enemySO.Health;
        vulnerable.Weight = enemySO.Weight;
        vulnerable.Friction = enemySO.Friction;
        rewardDrop.Rewards = enemySO.Rewards;
    }

    private Vector2 GetRandomPointOnBorder(BoxCollider2D boxCollider, float aggressiveness, EnemyTiming.SidesEnum side)
    {
        // Get the BoxCollider2D's position, size, and rotation
        Vector2 position = boxCollider.transform.position;
        Vector2 size = Vector2.Scale(boxCollider.size, boxCollider.transform.localScale);
        float rotation = boxCollider.transform.eulerAngles.z;

        // Calculate half extents of the rotated BoxCollider2D
        Vector2 halfExtents = size * 0.5f;
        float cosRotation = Mathf.Cos(rotation * Mathf.Deg2Rad);
        float sinRotation = Mathf.Sin(rotation * Mathf.Deg2Rad);
        float halfWidth = halfExtents.x;
        float halfHeight = halfExtents.y;

        // Get a random point on the border of the BoxCollider2D
        Vector2 randomPointLocal = Vector2.zero;

        if (side == EnemyTiming.SidesEnum.Random)
        {
            side = (EnemyTiming.SidesEnum)Random.Range(1, 5); // Randomly select a side of the box
        }
        

        switch (side)
        {
            case EnemyTiming.SidesEnum.Top: // Top side
                randomPointLocal = new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight);
                break;
            case EnemyTiming.SidesEnum.Bottom: // Bottom side
                randomPointLocal = new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight);
                break;
            case EnemyTiming.SidesEnum.Left: // Left side
                randomPointLocal = new Vector2(-halfWidth, Random.Range(-halfHeight, halfHeight));
                break;
            case EnemyTiming.SidesEnum.Right: // Right side
                randomPointLocal = new Vector2(halfWidth, Random.Range(-halfHeight, halfHeight));
                break;
        }

        // Rotate the local random point by the BoxCollider2D's rotation
        Vector2 randomPointRotated = new Vector2(
            randomPointLocal.x * cosRotation - randomPointLocal.y * sinRotation,
            randomPointLocal.x * sinRotation + randomPointLocal.y * cosRotation
        );
        // Transform the rotated point back to world space
        Vector2 randomPointWorld = position + randomPointRotated;

        var playerPosition=_gameManager.Player.transform.position;

        return Vector2.Lerp(randomPointWorld, playerPosition, aggressiveness);
    }
}

