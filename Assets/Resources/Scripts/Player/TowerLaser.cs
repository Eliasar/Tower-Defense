using UnityEngine;
using System.Collections;

public class TowerLaser : MonoBehaviour {

    public float speed;
    public float power;
    public float range;

    public GameObject target;

    private float distanceTraveled;

	void Start () {
        speed = 0.00001f;
        power = 2.0f;
        range = 10.0f;

        distanceTraveled = 0.0f;

        //print("IMMA FIRIN' AT " + target.transform.position);
	}
	
	void Update () {
        if (target != null) {
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            distanceTraveled += Time.deltaTime * speed;
        } else {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (distanceTraveled >= range)
            Destroy(gameObject);
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
