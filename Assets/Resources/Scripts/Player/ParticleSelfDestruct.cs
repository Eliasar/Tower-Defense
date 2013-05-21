using UnityEngine;
using System.Collections;

public class ParticleSelfDestruct : MonoBehaviour {

    private ParticleSystem ps;

	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	void Update () {
        if (!ps.IsAlive())
            Destroy(gameObject);
	}
}
