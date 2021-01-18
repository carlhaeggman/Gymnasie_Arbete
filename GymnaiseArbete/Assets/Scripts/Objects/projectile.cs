using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
   public float t;
   public ParticleSystem particleSstm;

   private void Start()
   {
        StartCoroutine(timedDestroy());
        
   }
   void DestroyProjectile()
   {
        Instantiate(particleSstm, transform.position, Quaternion.identity);
        Destroy(gameObject);
   }

    IEnumerator timedDestroy()
    {
        yield return new WaitForSeconds(t);
        DestroyProjectile();
    }
    private void OnCollisionEnter(Collision other)
    {
        DestroyProjectile();
    }


}
