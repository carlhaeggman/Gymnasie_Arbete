using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    float t;
    public ParticleSystem particleSstm;

    private void Start()
    {
        t = particleSstm.main.duration + 0.2f;
        StartCoroutine(timedDestroy());
    }
    void DestroyParticleSstm()
    {
        Destroy(gameObject);
    }

    IEnumerator timedDestroy()
    {
        yield return new WaitForSeconds(t);
        DestroyParticleSstm();
    }
}
