using UnityEngine;
using System.Collections;

public class DrifterLaser : MonoBehaviour {

    public int speed;
    public int power;

    private Hashtable ht;

	void Start () {
        speed = 4;
        power = 1;

        ht = new Hashtable();
        ht.Add("amount", new Vector3(-speed, 0, 0));
        ht.Add("time", 0);
	}
	
	void Update () {
        //iTween.MoveBy(gameObject, ht);

        // For bullets fired by ships offscreen
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            print("I hit the player!");
            Destroy(gameObject);
        }
    }
}
