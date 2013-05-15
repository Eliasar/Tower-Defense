using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Game : MonoBehaviour {

    public GameObject drifterPrefab;
    public GameObject enemyContainer;

    void Awake() {
        Application.targetFrameRate = 60;
    }

	public void Start() {
        LoadLevel(1);
	}
	
	void Update() { }

    public void LoadLevel(int level) {
        // if level 1, create x drifters y second apart
        if (level == 1) {
            float frequency = 0.5f;
            int amount = 15;
            float speed = drifterPrefab.GetComponent<Drifter>().speed;
            StartCoroutine(LevelCoroutine("drifter", frequency, amount, speed));
        }
    }

    IEnumerator LevelCoroutine(string type, float frequency, int amount, float speed = 1.0f) {
        
        Vector3 position = new Vector3(-15, 14, 0);
        
        for (int i = 0; i < amount; i++) {
            if (type.Equals("drifter")) {
                GameObject tempDrifter = CreateDrifter(position);
                tempDrifter.transform.parent = enemyContainer.transform;
                /*Sequence mySequence = new Sequence(new SequenceParms());
                TweenParms parms = new TweenParms();
                parms.AutoKill(true);
                parms.Ease(EaseType.Linear);

                parms.Prop("position", new Vector3(15, 14, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(15, 8, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(-15, 8, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(-15, 2, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(15, 2, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(15, -4, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(-15, -4, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(-15, -10, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                parms.Prop("position", new Vector3(15, -10, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 5 / speed, parms));
                parms.Prop("position", new Vector3(15, -14, 0));
                mySequence.Append(HOTween.To(tempDrifter.transform, 1 / speed, parms));
                mySequence.Play();*/
            }
                
            yield return new WaitForSeconds(frequency);
        }
    }

    GameObject CreateDrifter(Vector3 pos) {
        return (GameObject)Instantiate(drifterPrefab, drifterPrefab.transform.position, Quaternion.identity);
    }
}
