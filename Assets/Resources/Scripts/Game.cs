using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    /*public GameObject player;
    private Player playerComp;
    public GameObject drifterPrefab;
    public GameObject startText;
    public GameObject gameOverText;
    //public bool gameOverBool;
    public bool levelFinished;
    public int currentLevel;*/

    void Awake() {
        Application.targetFrameRate = 60;
    }

	void Start() {
        //LoadLevel(1);
        // Load up the first level board
        // Create a grid of cubes that fills the board size

	}
	
	void Update() {
        
        /*if (!player.activeSelf && playerComp.livesLeft > 0)
            RespawnPlayer();

        if (levelFinished) {
            levelFinished = false;

            print("Should be loading level " + currentLevel);
            LoadLevel(++currentLevel);
        }*/
	}

    void RespawnPlayer() {
        /*playerComp.respawnTimer += Time.deltaTime;
        if (playerComp.respawnTimer >= playerComp.respawnLimit) {
            playerComp.respawnTimer = 0.0f;
            player.SetActive(true);
            playerComp.init();
        }*/
    }

    /*public void GameOver() {
        Instantiate(gameOverText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        StartCoroutine(GameOverHelper());
    }*/

    /*private IEnumerator GameOverHelper() {
        yield return new WaitForSeconds(5.0f);
        Application.LoadLevel(0);
    }*/

    /*void CreateDrifter() {
        Instantiate(drifterPrefab, new Vector3(200, 0, 0), Quaternion.identity);
    }*/

    /*void LoadLevel(int level) {
        levelFinished = false;
        playerComp = player.GetComponent<Player>();
        startText.guiText.enabled = true;
        startText.guiText.text = "Level " + level;
        Instantiate(startText, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);

        // TODO: Level editor
        float frequency = 1.0f / currentLevel;
        if (frequency < 0.125f) frequency = 0.125f;
        int amount = 5 * currentLevel;
        if (amount > 50) amount = 50;

        // TODO: When powerups are created
        playerComp.shotInterval = .2f / currentLevel;
        if (playerComp.shotInterval < .05f) playerComp.shotInterval = .05f;

        StartCoroutine(LevelCoroutine("drifter", frequency, amount));
    }*/

    /*IEnumerator LevelCoroutine(string type, float frequency, int amount) {
        for (int i = 0; i < amount; i++) {
            if(type.Equals("drifter")) CreateDrifter();
            yield return new WaitForSeconds(frequency);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) {
            yield return null;
        }

        levelFinished = true;
    }*/
}
