using System.Collections;
<<<<<<< HEAD
=======
using UnityEngine;
>>>>>>> dd73683a054e31cb8f220a884410596b0fa3c45b

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
        currentWave = 1;

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

<<<<<<< HEAD
    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f) {
        
        Vector3 position = new Vector3(-14, 14, 0);
        
        for (int i = 0; i < amount; i++) {
            if (type.Equals("drifter")) {
                GameObject tempDrifter = CreateDrifter(position);
                tempDrifter.transform.parent = enemyContainer.transform;
=======
    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f, int waves = 5) {

        // Initial delay
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < waves; i++) {
            for (int j = 0; j < amount; j++) {
                if (type.Equals("drifter")) {
                    GameObject tempDrifter = CreateDrifter(Vector3.zero);
                    tempDrifter.transform.parent = enemyContainer.transform;
                    tempDrifter.GetComponent<Enemy>().HP *= 1 + (i * 0.1f);
                }

                yield return new WaitForSeconds(frequency);
>>>>>>> dd73683a054e31cb8f220a884410596b0fa3c45b
            }

            // After wave delay
            while (enemyContainer.transform.childCount > 0) {
                yield return new WaitForSeconds(0.5f);
            }

            // Increment wave
            currentWave++;
        }
    }

    GameObject CreateDrifter(Vector3 pos) {
        return (GameObject)Instantiate(drifterPrefab, drifterPrefab.transform.position, Quaternion.identity);
    }
}
