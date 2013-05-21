using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public GameObject drifterPrefab;
    public GameObject enemyContainer;

    // Keep track of Score, Cash, Lives, and Waves
    public int score;
    public int cash;
    public int lives;
    public int currentWave;

    void Awake() {
        Application.targetFrameRate = 60;
    }

	public void Start() {

        // Score, Cash, Lives, and Waves
        score = 0;
        cash = 100;
        lives = 10;
        currentWave = 1;

        LoadLevel(1);
	}
	
	void Update() {
        
        // Lose condition
        //if (lives <= 0) { }
    }

    public void LoadLevel(int level) {
        // if level 1, create x drifters y second apart
        if (level == 1) {
            float frequency = 0.5f;
            int amount = 15;
            float speed = drifterPrefab.GetComponent<Drifter>().speed;
            int waves = 1;
            StartCoroutine(LevelCoroutine("drifter", frequency, amount, speed, waves));
        }
    }

    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f, int waves = 5) {
        
        Vector3 position = new Vector3(-14, 14, 0);
        
        for (int i = 0; i < waves; i++) {
            for (int j = 0; j < amount; j++) {
                if (type.Equals("drifter")) {
                    GameObject tempDrifter = CreateDrifter(position);
                    tempDrifter.transform.parent = enemyContainer.transform;
                }
            }

            yield return new WaitForSeconds(frequency);
        }

        // After-wave delay
        while (enemyContainer.transform.childCount > 0) {
            yield return new WaitForSeconds(0.5f);
        }

        // Increment wave
        currentWave++;
    }

    GameObject CreateDrifter(Vector3 pos) {
        return (GameObject)Instantiate(drifterPrefab, drifterPrefab.transform.position, Quaternion.identity);
    }
}
