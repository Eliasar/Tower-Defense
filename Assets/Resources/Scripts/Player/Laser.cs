using UnityEngine;
using System.Collections;

public class Laser : TowerProjectile {

    public Vector3 targetVelocity;
    public float distance;
    public float timeToTarget;
    public Vector3 positionToAimAt;

    protected override void Start() {

        targetVelocity = target.GetComponent<Enemy>().velocity / Time.deltaTime;
        distance = Vector3.Distance(transform.position, target.transform.position);
        timeToTarget = distance / speed;
        positionToAimAt = target.transform.position + (targetVelocity * timeToTarget);

        transform.LookAt(positionToAimAt);
    }

	protected override void LateUpdate () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    protected override void SelfDestruct() {
        base.SelfDestruct();
    }
}
