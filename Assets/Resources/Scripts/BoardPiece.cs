using UnityEngine;
using System.Collections;

public class BoardPiece : MonoBehaviour {

    public Material materialSelected;
    public Material materialOriginal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter() { renderer.material = materialSelected; }
    void OnMouseExit() { renderer.material = materialOriginal; }
}
