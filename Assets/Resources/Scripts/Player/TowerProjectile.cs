using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TowerProjectile : MonoBehaviour {

    public float speed;
    public float power;

    public GameObject target;
    private Vector3 targetVelocity;
    private float distanceFromTarget;
    private float leadDistance;

	void Start () {
        targetVelocity = target.GetComponent<Enemy>().velocity;
        distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
        leadDistance = distanceFromTarget;
	}
	
	void LateUpdate () {
        if (target) {
            Debug.DrawLine(transform.position, target.transform.position, Color.white);

            // Calculate lead
            /*leadDistance = distanceFromTarget;
            leadDistance += 

            transform.LookAt(target.transform);*/
            rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
        else {
            rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

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
