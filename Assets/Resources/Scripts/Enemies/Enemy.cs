using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float HP;
    public GameObject explosionPrefab;

    // To calculate 'velocity'
    private Vector3 lastPosition;
    //private float currPosition;
    public Vector3 velocity;

	void Start () {
        lastPosition = transform.position;
        velocity = Vector3.zero;
	}

    protected virtual void Update() {
        // Calculate speed
        velocity = transform.position - lastPosition;

        // Update lastPosition
        lastPosition = transform.position;
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
