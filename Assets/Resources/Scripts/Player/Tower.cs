using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    // Self stats
    public int ID;
    public int level;
    public float experience;
    public string type;
    public int range;
    public int power;
    public float rateOfFire;            // set in inspector (unit: 1/s)

    public GameObject projectilePrefab; // set in inspector
    public float shotInterval;          // set in inspector (unit: s)
    public float shotTimer;
    public bool canFire;
    public float rotationSpeed;
    public string targetType;
    public int cost;                    // set in inspector

    public List<GameObject> enemiesInRange;
    public GameObject closestEnemy;

	void Start () {
        shotTimer = shotInterval;
        canFire = true;
        rotationSpeed = 3.0f;
        level = 0;
        experience = 0.0f;
        range = 5;
        power = 1;

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
            switch (targetType) { 
                case "First":
                    closestEnemy = enemiesInRange[0];
                    break;
                case "Last":
                    closestEnemy = enemiesInRange[enemiesInRange.Count - 1];
                    break;
                case "Strong":
                case "Weak":
                default:
                    FindClosestInList();
                    break;
            }

            if (closestEnemy) {
                // Look at closest
                RotateToTarget();

                Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.green);
                
                //Fire!
                if (canFire)
                    Fire(closestEnemy);
            }
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

    void RotateToTarget() {
        if(closestEnemy)
            transform.LookAt(closestEnemy.transform);
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

        // Calculate direction of target
        Vector3 dir = transform.rotation * Vector3.forward;
        
        // Fire ze missles!
        GameObject temp = Instantiate(projectilePrefab,
            transform.position + dir,
            transform.rotation) as GameObject;
        temp.GetComponent<TowerProjectile>().target = target;
        temp.GetComponent<TowerProjectile>().ID = ID;

        // Reset timer and canFire
        canFire = false;
        shotTimer = 0.0f;
    }
}
