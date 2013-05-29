using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour {

    // General Stats
    public float HP;
    private float startingHP;
    public UISlider healthBar;
    public int cashValue;               // set in inspector
    public float experienceValue;       // calculated in CalculateExperience
    private bool takeLife;

    public GameObject explosionPrefab;  // set in inspector

    // Used for tower shot leading
    private Vector3 lastPos;
    public Vector3 velocity;

    // Used for pathfinding
    private Vector3 targetPosition;
    private Seeker seeker;
    private Path path;
    public float speed;                 // set in inspector
    private float nextWaypointDistance;
    private int currentWaypoint = 0;

    void Awake() {
        startingHP = HP;
        lastPos = transform.position;
        seeker = GetComponent<Seeker>();
        nextWaypointDistance = 0.1f;
        takeLife = false;

        experienceValue = Mathf.Log(GameObject.Find("_Game Master").GetComponent<Game>().currentWave);
        experienceValue = Mathf.Round(experienceValue);
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
        if (col.gameObject.CompareTag("Player Laser")) {
            Hit(col.gameObject.GetComponent<TowerProjectile>().power);
            
            // Award exp to tower
            print("Hit by ID: " + col.gameObject.GetComponent<TowerProjectile>().ID);
            GameObject[] towerList = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject tower in towerList) { 
                if(tower.GetComponent<Tower>().ID == col.gameObject.GetComponent<TowerProjectile>().ID) {
                    tower.GetComponent<Tower>().experience +=
                        col.gameObject.GetComponent<TowerProjectile>().power / startingHP;
                }
            }
        }
        if (col.gameObject.CompareTag("End Zone")) {
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
