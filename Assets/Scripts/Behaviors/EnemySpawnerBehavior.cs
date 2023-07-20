using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : AthenaMonoBehavior
{
    [SerializeField]
    private BoxCollider2D _spawnBoundry;
    [SerializeField]
    private float _spawnRate;
    [SerializeField]
    private List<EnemySO> _enemies;

    private GameManagerBehavior.TimedEvent _timedEvent;
    //private Vector3 _trueBoundingScale=Vector3.one;

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

        SetSpawnRate(_spawnRate);
    }

    private void SetSpawnRate(float rate)
    {
        if (_timedEvent == null)
        {
            _timedEvent = _gameManager.AddTimedEvent(rate, () =>
            {
                var enemy = _enemies[Random.Range(0,_enemies.Count)];

                SpawnEnemy(enemy);

            }, gameObject);
        } 
    }

    private void SpawnEnemy(EnemySO _enemy)
    {
        var newPosition = GetRandomPointOnBorder(_spawnBoundry);
        var enemy = _gameManager.Pool.GetPooledObject(_enemy.Prefab, newPosition, Quaternion.identity);
        var flying = enemy.GetComponent<FlyingBehavior>();
        var damaging = enemy.GetComponent<DamagingBehavior>();
        var vulnerable = enemy.GetComponent<VulnerableBehavior>();

        flying.Speed = _enemy.Speed;
        damaging.Damage = _enemy.TouchDamage;
        vulnerable.MaxHealth = _enemy.Health;
    }

    private Vector2 GetRandomPointOnBorder(BoxCollider2D boxCollider)
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

        float side = Random.Range(0, 4); // Randomly select a side of the box

        switch (side)
        {
            case 0: // Top side
                randomPointLocal = new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight);
                break;
            case 1: // Bottom side
                randomPointLocal = new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight);
                break;
            case 2: // Left side
                randomPointLocal = new Vector2(-halfWidth, Random.Range(-halfHeight, halfHeight));
                break;
            case 3: // Right side
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

        return randomPointWorld;
    }
}

