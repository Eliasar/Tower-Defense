using UnityEngine;
using System.Collections;

public class TowerProjectile : MonoBehaviour {

    public float speed;
    public float power;

    public GameObject target;

    public ParticleSystem ps;

	protected virtual void Start() { }

    protected virtual void LateUpdate() { }

    void OnBecameInvisible() {
        SelfDestruct();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
            target = null;
            SelfDestruct();
        }
    }

    void SelfDestruct() {
        ps = transform.FindChild("Particle Holder").GetComponent<ParticleSystem>();
        ps.transform.parent = null;
        ps.GetComponent<ParticleAnimator>().autodestruct = true;
        ps.Stop();
        Destroy(gameObject);
    }
}
