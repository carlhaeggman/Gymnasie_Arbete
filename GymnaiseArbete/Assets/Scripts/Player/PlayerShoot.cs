using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{   
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public float accuracy, shootForce, maxAmmo, fireRate, damage;

    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    float bulletLifeTime = 3.0f;
   
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform rayOrigin;
    public Transform rayDestination;
    public LayerMask playerLayer;

    List<Bullet> bullets = new List<Bullet>();

    Ray ray;
    RaycastHit hit;



    private void Start()
    {
        damage = 50f;
    }

    void Update()
    {
        UpdateBullets(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(autoShoot());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopFiring();
        }
    }

   public void Shoot()
   {
        

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (rayDestination.position - rayOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(rayOrigin.position, velocity);
        bullets.Add(bullet);
    }
    IEnumerator autoShoot()
    {
        while (Input.GetButton("Fire1"))
        {
            Shoot();
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void StopFiring()
    {
        StopAllCoroutines();
    }


    Vector3 GetPosition(Bullet bullet)
    {
        //p+v*t+0.5+g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
               //Initial Position                    Initial velocity                            Bulletdrop(Gravity)
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);


    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RayCastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= bulletLifeTime);
    }

    void RayCastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hit, distance, ~playerLayer))
        {
            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);
            
            bullet.tracer.transform.position = hit.point;
            bullet.time = bulletLifeTime;
         
            if(hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("HELLO");
                hit.collider.gameObject.GetComponentInParent<Stats>().TakeDamage(damage);
            }
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }
}
