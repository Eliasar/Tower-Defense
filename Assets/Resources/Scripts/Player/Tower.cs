using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    public GameObject laserPrefab;
    public float shotInterval;
    public float shotTimer;
    public bool canFire;
    public GameObject closestEnemy;
    public float closestDistance;

	// Use this for initialization
	void Start () {
        shotTimer = 0.0f;
        canFire = true;
        closestDistance = 0.0f;
        closestEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (shotTimer < shotInterval) {
            shotTimer += Time.deltaTime;
            canFire = false;
        }
        else {
            shotTimer = 0.0f;
            canFire = true;
        }

        if (closestEnemy) {
            closestDistance = Vector3.Distance(closestEnemy.transform.position,
                                           transform.position);
        }
	}

    void OnCollisionStay(Collision col) {
        if (canFire) {
            if (col.gameObject.CompareTag("Enemy")) {
                float distance = Vector3.Distance(col.gameObject.transform.position,
                                                  transform.position);
                print("Distance = " + distance + " and closest distance = " + closestDistance);

                if (closestDistance == 0) {
                    closestDistance = distance;
                    closestEnemy = col.gameObject;
                }
                else if (distance < closestDistance) {
                    print("Found a lower distance! (" + distance + "):(" + closestDistance + ")");
                    closestDistance = distance;
                    closestEnemy = col.gameObject;
                }
                else if (distance > transform.GetComponent<SphereCollider>().radius || 
                    !closestEnemy) {
                    print("Out of range. Resetting closest distance and object");
                    closestDistance = 0.0f;
                    closestEnemy = null;
                }
                else {
                    print("No closer distance found. Enemy @ " + closestEnemy.transform.position);
                }

                // TODO: fix. Will fire x times where x = number of enemies currently in range.
                if(distance == closestDistance)
                    Fire(closestEnemy);
            }
        }
    }

    void Fire(GameObject enemyGameObject) {
        GameObject temp = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        temp.GetComponent<TowerLaser>().drifterPrefab = enemyGameObject;
    }
}
