using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : MonoBehaviour {
    public ParticleSystem ps;
    // Use this for initialization
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>3)
        {
            Destroy(gameObject);
        }
    }

}
