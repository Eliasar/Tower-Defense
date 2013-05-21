using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour {

    public float HP;
    private float startingHP;
    public UISlider healthBar;

    public GameObject explosionPrefab;

    private Vector3 lastPos;
    public Vector3 velocity;

    public Vector3 targetPosition;
    public Seeker seeker;
    public Path path;
    public float speed = 100.0f;
    public float nextWaypointDistance = 3.0f;
    private int currentWaypoint = 0;

    void Awake() {
        startingHP = HP;
        lastPos = transform.position;
    }

	void Start() {
        targetPosition = GameObject.FindWithTag("GroundTargetObject").transform.position;
        GetNewPath();

        if (healthBar == null) {
            Debug.LogError("Could not find the UISlider Component!");
            return;
        }

        UpdateHealth();
	}

    void GetNewPath() {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    void OnPathComplete(Path newPath) {
        if (!newPath.error) {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate() {
        if (path == null) {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count) {
            return;
        }

        // Find direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //controller.SimpleMove(dir);
        transform.position += dir;

        // Face direction of next waypoint
        //tankCompass.LookAt(path.vectorPath[currentWaypoint]);
        //tankBody.rotation(Quaternion.Lerp(tankBody.rotation, tankCompass.rotation, Time.deltaTime * turnSpeed));

        // Check if we are close enough to the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
        }
    }

    protected virtual void Update() {
        velocity = transform.position - lastPos;
        lastPos = transform.position;
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
            StartCoroutine(DelayedDestroy(healthBar.gameObject));
            StartCoroutine(DelayedDestroy(gameObject));
        }
        UpdateHealth();
    }

    void UpdateHealth() {
        if (HP > 0) {
            healthBar.GetComponent<UISlider>().sliderValue = HP / startingHP;
        }
    }

    IEnumerator DelayedDestroy(GameObject obj) {
        yield return new WaitForEndOfFrame();
        Destroy(obj);
    }
}
