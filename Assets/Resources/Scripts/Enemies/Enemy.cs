using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float HP;
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

    void Hit(float powerOfHit) {
        HP -= powerOfHit;
        if (HP <= 0) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Player")) {
                temp.GetComponent<Tower>().enemiesInRange.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
