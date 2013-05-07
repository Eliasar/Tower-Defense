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

	// Use this for initialization
	void Start () {
        shotTimer = 0.0f;
        canFire = true;

        enemiesInRange = new List<GameObject>();
        closestEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (shotTimer < shotInterval) {
            shotTimer += Time.deltaTime;
            canFire = false;
        }
        else {
            canFire = true;
        }

        // Find closest in list
        if (canFire && enemiesInRange.Count > 0) {
            FindClosestInList();
            Fire(closestEnemy);
        }
	}

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
            print(col.gameObject + " has come into range.");
            enemiesInRange.Add(col.gameObject);
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
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
                    print("New minimum distance: " + distance);
                    minimumDistance = distance;
                    closestEnemy = enemy;
                }
            } else {
                Destroy(enemy);
            }
        }
    }

    void Fire(GameObject target) {
        
        // Fire ze missles!
        GameObject temp = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        temp.GetComponent<TowerLaser>().target = target;
        print("Target @ " + target.transform.position);

        // Reset timer and canFire
        canFire = false;
        shotTimer = 0.0f;
    }
}
