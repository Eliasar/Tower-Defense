using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float HP;
    public GameObject explosionPrefab;

    public Vector3 lastPos;
    public Vector3 velocity;

	void Start () {
        lastPos = transform.position;
	}

    protected virtual void Update() {
        velocity = transform.position - lastPos;
        lastPos = transform.position;
    }

    protected virtual void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
            if (col.gameObject.CompareTag("Player Laser"))
                Hit(col.gameObject.GetComponent<TowerProjectile>().power);
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
