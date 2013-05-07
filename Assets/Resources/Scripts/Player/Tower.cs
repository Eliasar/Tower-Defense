using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    public GameObject laserPrefab;
    public float shotInterval;
    public float shotTimer;
    public bool canFire;

    public List<GameObject> enemiesInRange;
    public GameObject closestEnemy;

	void Start () {
        shotTimer = shotInterval;
        canFire = true;

        enemiesInRange = new List<GameObject>();
        closestEnemy = null;
	}
	
	void LateUpdate () {
        
        // Update shot interval and canFire
        if (shotTimer < shotInterval) {
            shotTimer += Time.deltaTime;
            canFire = false;
        }
        else {
            canFire = true;
        }

        // Find closest in list
        if (enemiesInRange.Count > 0) {
            FindClosestInList();

            // Look at closest
            transform.LookAt(closestEnemy.transform);
            Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.blue);

            //Fire!
            if (canFire)
                Fire(closestEnemy);
        }
        else {
            closestEnemy = null;
        }
	}

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Enemy")) {
            enemiesInRange.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Enemy")) {
            enemiesInRange.Remove(col.gameObject);
        }
    }

    void FindClosestInList() {

        // Given a list, find the closest distance to the tower
        float minimumDistance = 100.0f;

        foreach (GameObject enemy in enemiesInRange) {
            if (enemy) {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);

                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    closestEnemy = enemy;
                }
            } else {
                Destroy(enemy);
                minimumDistance = 100.0f;
            }
        }
    }

    void Fire(GameObject target) {
        
        // Fire ze missles!
        GameObject temp = Instantiate(laserPrefab,
            transform.position,
            transform.rotation) as GameObject;
        temp.GetComponent<TowerProjectile>().target = target;

        // Reset timer and canFire
        canFire = false;
        shotTimer = 0.0f;
    }
}
