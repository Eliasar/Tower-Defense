using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject drifterPrefab;
    public GameObject enemyContainer;
    public int cash;
    public int score;
    public int lives;
    public int currentWave;

    void Awake() {
        Application.targetFrameRate = 60;
    }

	public void Start() {
        // Init lives, cash, etc.
        lives = 10;
        cash = 100;
        score = 0;
        currentWave = 0;

        // Load level
        LoadLevel(1);
	}
	
	void Update() {
        if (lives <= 0)
            Quit();
    }

    void Quit() { 
        Application.Quit();
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

    public void StartNewWave() {
        LoadLevel(1);
    }

    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f, int waves = 5) {

        // Initial delay
        yield return new WaitForSeconds(1.0f);

        // Increment wave
        currentWave++;

        for (int i = 0; i < waves; i++) {
            for (int j = 0; j < amount; j++) {
                if (type.Equals("drifter")) {
                    GameObject tempDrifter = CreateDrifter(Vector3.zero);
                    tempDrifter.transform.parent = enemyContainer.transform;
                    tempDrifter.GetComponent<Enemy>().HP *= 1 + (i * 0.1f);
                }

                yield return new WaitForSeconds(frequency);
            }

            // After wave delay
            while (enemyContainer.transform.childCount > 0) {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    GameObject CreateDrifter(Vector3 pos) {
        return (GameObject)Instantiate(drifterPrefab, drifterPrefab.transform.position, Quaternion.identity);
    }
}
