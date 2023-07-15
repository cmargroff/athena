using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : AthenaMonoBehavior
{
  public GameObject bulletPrefab;
  public float interval = 0.1f;
  public float angle = 0f;
  public float velocity = 10f;
  public float damage = 1f;
  public float lifetime = 1f;
  public float bulletSize = 0.2f;
  private Vector3 _vector = new Vector3(0, 0, 0);
  private ParticleSystem _particles;
  List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
  List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
  public List<ParticleCollisionEvent> collisionEvents;

  protected override void Start()
  {
    base.Start();
    collisionEvents = new List<ParticleCollisionEvent>();
    SetAngle(angle);
    _particles = GetComponent<ParticleSystem>();
  }
  public void SetAngle(float angle)
  {
    this.angle = angle;
    _vector.x = Mathf.Cos(angle) * velocity;
    _vector.z = Mathf.Sin(angle) * velocity;
  }

  // Update is called once per frame
  // protected override void PausibleFixedUpdate()
  // {
  //   if (Time.realtimeSinceStartup % interval < 0.04f)
  //   {

  //     GameObject bullet = Instantiate(bulletPrefab);
  //     bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
  //     var behavior = bullet.GetComponent<BulletBehavior>();
  //     behavior.direction = _vector;
  //     behavior.lifetime = lifetime;
  //     behavior.damage = damage;
  //     // bullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

  //     SetAngle(Random.Range(0, 360) * Mathf.Deg2Rad);
  //   }
  // }
  public void OnParticleTrigger()
  {
    int numEnter = _particles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    // int numExit = _particles.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    Debug.Log("Particle Triggered!");
    // iterate through the particles which entered the trigger and make them red
    for (int i = 0; i < numEnter; i++)
    {
      ParticleSystem.Particle p = enter[i];
      p.startColor = new Color32(255, 0, 0, 255);
      enter[i] = p;
    }

    // iterate through the particles which exited the trigger and make them green
    // for (int i = 0; i < numExit; i++)
    // {
    //   ParticleSystem.Particle p = exit[i];
    //   p.startColor = new Color32(0, 255, 0, 255);
    //   exit[i] = p;
    // }

    // re-assign the modified particles back into the particle system
    _particles.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    // _particles.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
  }
  void OnParticleCollision(GameObject other)
  {
    int numCollisionEvents = _particles.GetCollisionEvents(other, collisionEvents);
    Debug.Log(other.name + " hit by " + gameObject.name + "!");

    Rigidbody rb = other.GetComponent<Rigidbody>();
    int i = 0;

    while (i < numCollisionEvents)
    {
      if (rb)
      {
        Vector3 pos = collisionEvents[i].intersection;
        Vector3 force = collisionEvents[i].velocity * 10;
        rb.AddForce(force);
      }
      i++;
    }
  }
}
