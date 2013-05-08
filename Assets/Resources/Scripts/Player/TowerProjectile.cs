using UnityEngine;
using System.Collections;

public class TowerProjectile : MonoBehaviour {

    public float speed;
    public float power;

    public GameObject target;

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

    protected virtual void SelfDestruct() {        
        Destroy(gameObject);
    }
}
