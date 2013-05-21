using UnityEngine;
using System.Collections;

public class Flame : TowerProjectile {

    public Vector3 targetVelocity;
    public float distance;
    public float timeToTarget;
    public Vector3 positionToAimAt;
    public float range;
    public float distanceTraveled;

    protected override void Start() {
        distanceTraveled = 0.0f;
        targetVelocity = target.GetComponent<Enemy>().velocity / Time.deltaTime;
        distance = Vector3.Distance(transform.position, target.transform.position);
        timeToTarget = distance / speed;
        positionToAimAt = target.transform.position + (targetVelocity * timeToTarget);

        transform.LookAt(positionToAimAt);
    }

	protected override void LateUpdate () {
        if (distanceTraveled < range) {
            transform.position += transform.forward * speed * Time.deltaTime;
            distanceTraveled += speed * Time.deltaTime;
        } else {
            SelfDestruct();
        }
	}

    protected override void SelfDestruct() {
        base.SelfDestruct();
    }
}
