using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Game : MonoBehaviour {

    public GameObject boardPiece;
    public GameObject drifterPrefab;
    public GameObject tower;
    public GameObject towerLaser;

    void Awake() {
        Application.targetFrameRate = 60;
    }

	void Start() {
        // Load up the first level board
        LoadLevel(1);
	}
	
	void Update() {
        /*if (Input.GetKey("space")) { 
            // Find closest drifter and fire at him
            float minimumDistance = 0.0f;
            //GameObject closestDrifter;

            foreach (GameObject drifter in GameObject.FindGameObjectsWithTag("Enemy")) {
                Vector3 diff = drifter.transform.position - tower.transform.position;

                if (diff.sqrMagnitude < minimumDistance) {
                    minimumDistance = diff.sqrMagnitude;
                    closestDrifter = drifter;
                }
            }
            //towerLaser = new GameObject

            //Instantiate(towerLaser, tower.transform.position, Quaternion.identity);
        }*/
	}

    void LoadLevel(int level) {
        // if level 1, create 5 drifters 1 second apart
        if (level == 1) {
            float frequency = 1.0f;
            int amount = 10;
            float speed = drifterPrefab.GetComponent<Drifter>().speed;
            StartCoroutine(LevelCoroutine("drifter", frequency, amount, speed));
        }
    }

    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f) {
        
        Vector3 position = new Vector3(-16, 16, 0);
        
        for (int i = 0; i < amount; i++) {
            if (type.Equals("drifter")) {
                GameObject tempDrifter = CreateDrifter(position);
                Sequence mySequence = new Sequence(new SequenceParms());
                TweenParms parms = new TweenParms();
                parms.Ease(EaseType.Linear);

                parms.Prop("position", new Vector3(14, 16, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(14, 10, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(-16, 10, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(-16, 4, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(14, 4, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(14, -2, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(-16, -2, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(-16, -8, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(14, -8, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(14, -14, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                mySequence.Play();
            }
                
            yield return new WaitForSeconds(frequency);
        }
    }

    GameObject CreateDrifter(Vector3 pos) {
        return (GameObject)Instantiate(drifterPrefab, pos, Quaternion.identity);
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
