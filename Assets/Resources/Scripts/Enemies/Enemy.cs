using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour {

    public float HP;
    private float startingHP;
    public UISlider healthBar;
    public int cashValue;               // set in inspector

    public GameObject explosionPrefab;  // set in inspector

    private Vector3 lastPos;
    public Vector3 velocity;

    private Vector3 targetPosition;
    private Seeker seeker;
    private Path path;
    public float speed;                 // set in inspector
    private float nextWaypointDistance;
    private int currentWaypoint = 0;
    private bool takeLife;

    void Awake() {
        startingHP = HP;
        lastPos = transform.position;
        seeker = GetComponent<Seeker>();
        nextWaypointDistance = 0.1f;
        takeLife = false;
    }

    void Start() {
        targetPosition = GameObject.FindWithTag("End Zone").transform.position;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);

        if (healthBar == null) {
            Debug.LogError("Could not find the UISlider Component!");
            return;
        }

        UpdateHealth();
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

        if (takeLife) {
            print("Take life!");
            GameObject.Find("_Game Master").GetComponent<Game>().lives--;
            takeLife = !takeLife;
        }
    }

    protected virtual void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
            if (col.gameObject.CompareTag("Player Laser"))
                Hit(col.gameObject.GetComponent<TowerProjectile>().power);
            if (col.gameObject.CompareTag("End Zone")) {
                print("Got to the end." + Time.time);
                Hit(HP);
                takeLife = true;
            }
    }

    void Hit(float powerOfHit) {
        HP -= powerOfHit;
        if (HP <= 0) {

            // Create Explosion
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Player")) {
                temp.GetComponent<Tower>().enemiesInRange.Remove(gameObject);
            }

            // Send cash to Game
            GameObject.Find("_Game Master").GetComponent<Game>().cash += cashValue;

            // Delayed Destroy
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
