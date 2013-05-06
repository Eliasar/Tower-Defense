using UnityEngine;
using System.Collections;

public class TowerLaser : MonoBehaviour {

    public int speed;
    public int power;

    public GameObject drifterPrefab;

    private float startTime;
    private float journeyLength;

	void Start () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(drifterPrefab.transform.position,
                                         transform.position);
	}
	
	void Update () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position,
                                          drifterPrefab.transform.position,
                                          fracJourney);
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
