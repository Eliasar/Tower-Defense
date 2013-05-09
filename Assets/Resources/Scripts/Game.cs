using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Game : MonoBehaviour {

    public GameObject boardPiece;
    public GameObject drifterPrefab;

    void Awake() {
        Application.targetFrameRate = 60;
    }

	void Start() {
        LoadLevel(1);
	}
	
	void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(mRay.origin.x, mRay.origin.y, 1);
            print("Placed object @ " + obj.transform.position);
        }
	}

    void LoadLevel(int level) {
        // if level 1, create x drifters y second apart
        if (level == 1) {
            float frequency = 0.5f;
            int amount = 100;
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
}
