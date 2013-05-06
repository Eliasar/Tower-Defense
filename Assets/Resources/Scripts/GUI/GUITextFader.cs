using UnityEngine;
using System.Collections;

public class GUITextFader: MonoBehaviour {

    public Color textColor;
    public float lifeTime;

	// Use this for initialization
	void Start () {
        gameObject.guiText.material.color = textColor;
	}
	
	// Update is called once per frame
	void Update () {
        if (textColor.a > 0) {
            float rightSideOfEquation = Time.deltaTime / lifeTime;
            textColor.a -= rightSideOfEquation;
            gameObject.guiText.fontSize += 1;

            gameObject.guiText.material.color = textColor;
        }
        else {
            Destroy(gameObject);
        }
	}
}
