using UnityEngine;
using System.Collections;

public class Missile : TowerProjectile {

    public Vector3 forward;

	protected override void LateUpdate () {
        speed += Time.deltaTime*5;
        if (target) {
            transform.LookAt(target.transform);
            float step = speed * Time.deltaTime;
            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, step);
            Debug.DrawLine(transform.position, target.transform.position);
            forward = transform.forward;
        } else {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    protected override void SelfDestruct() {
        base.SelfDestruct();

        if (transform.FindChild("Missile Trail")) {
            Transform ps = transform.FindChild("Missile Trail");
            ps.particleSystem.Stop();
            ps.parent = null;
            Destroy(ps.gameObject, 1.0f);
            //ps.Stop();

            //ps.transform.parent = null;
            //Destroy(ps.gameObject, 1.0f);
        }
    }
}
