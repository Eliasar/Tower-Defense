using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float HP;
    private float startingHP;
    public UISlider healthBar;
    //private Component _healthBarComponent;

    public GameObject explosionPrefab;

    private Vector3 lastPos;
    public Vector3 velocity;

    void Awake() {
        startingHP = HP;
        //healthBar = GetComponent<UISlider>();

        lastPos = transform.position;
    }

	void Start() {
        if (healthBar == null) {
            Debug.LogError("Could not find the UISlider Component!");
            return;
        }

        UpdateHealth();
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
