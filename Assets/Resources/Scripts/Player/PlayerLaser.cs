using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

    public int speed;
    public int power;

	void Start () {
        speed = 10;
        power = 1;
	}
	
	void Update () {
        iTween.MoveBy(gameObject, new Vector3(speed, 0, 0), 0);
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
