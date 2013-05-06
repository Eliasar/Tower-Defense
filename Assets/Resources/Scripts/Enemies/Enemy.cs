using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int HP;
    public GameObject explosionPrefab;

	void Start () {

	}

    protected virtual void Update() {

    }

    protected virtual void OnBecameInvisible() {

    }

    void OnCollisionEnter(Collision col) {
            if (col.gameObject.CompareTag("Player Laser"))
                Hit(col.gameObject.GetComponent<TowerLaser>().power);
    }

    void Hit(int powerOfHit) {
        HP -= powerOfHit;
        if (HP <= 0) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
