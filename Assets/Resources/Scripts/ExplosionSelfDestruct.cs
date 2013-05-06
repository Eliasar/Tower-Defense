using UnityEngine;
using System.Collections;

public class ExplosionSelfDestruct : MonoBehaviour {

    private ParticleSystem ps;

	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	void Update () {
        if (ps.isStopped) 
            Destroy(gameObject);
	}
}
