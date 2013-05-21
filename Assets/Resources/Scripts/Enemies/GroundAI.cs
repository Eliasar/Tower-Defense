using UnityEngine;
using System.Collections;
using Pathfinding;

public class GroundAI : MonoBehaviour {

    //public Transform tankBody;
    //public Transform tankCompass;
    //public float turnSpeed = 10.0f;

    public Vector3 targetPosition;
    public Seeker seeker;
    public CharacterController controller;
    public Path path;
    public float speed = 100.0f;
    public float nextWaypointDistance = 3.0f;
    private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
        targetPosition = GameObject.FindWithTag("GroundTargetObject").transform.position;
        GetNewPath();
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

        controller.SimpleMove(dir);
        //transform.position += dir;

        // Face direction of next waypoint
        //tankCompass.LookAt(path.vectorPath[currentWaypoint]);
        //tankBody.rotation(Quaternion.Lerp(tankBody.rotation, tankCompass.rotation, Time.deltaTime * turnSpeed));

        // Check if we are close enough to the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
        }
	}
}
