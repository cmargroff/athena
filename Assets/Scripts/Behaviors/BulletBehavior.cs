using UnityEngine;

public class BulletBehavior : AthenaMonoBehavior
{
  public Texture2D texture;
  public Vector3 direction;
  public float lifetime = 1f;
  public float damage = 1f;
  public int health = 1;

//   protected override void Start()
//   {
//     base.Start();
//     var collider = gameObject.AddComponent<SphereCollider>();
//     collider.OnTriggerEnter += OnTriggerEnter;
//   }
  protected override void PausibleFixedUpdate()
  {
    transform.position += direction;
    lifetime -= Time.deltaTime;
    if (lifetime < 0)
    {
      Destroy(gameObject);
    }
  }
  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Bullet hit "+other.name+"!");
    health--;
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }
}
