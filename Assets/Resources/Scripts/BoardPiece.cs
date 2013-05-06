using UnityEngine;
using System.Collections;

public class BoardPiece : MonoBehaviour {

    public Material materialSelected;
    public Material materialOriginal;
    public bool exitSuspended;
    public bool buildState;

    public Vector3 gridSize = new Vector3(1, 1, 1);
    public Vector3 movementDirection = new Vector3(0, -1, 0);

	// Use this for initialization
	void Start () {
        /*exitSuspended = false;
        buildState = false;*/

        InvokeRepeating("UpdatePosition", 1.0f, 1.0f);
	}

    IEnumerator UpdatePosition() {
        Vector3 newPos = transform.position + movementDirection;
        newPos = new Vector3(Mathf.Round(newPos.x / gridSize.x) * gridSize.x,
                            Mathf.Round(newPos.y / gridSize.y) * gridSize.y,
                            Mathf.Round(newPos.z / gridSize.z) * gridSize.z);
        transform.position = newPos;

        return null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {

        // Force all board pieces to be unselected
        foreach (GameObject otherBoardPiece in GameObject.FindGameObjectsWithTag("Board Piece")) {
            otherBoardPiece.GetComponent<BoardPiece>().exitSuspended = false;
            otherBoardPiece.GetComponent<BoardPiece>().ResetColor();
        }

        // Toggle material
        exitSuspended = !exitSuspended;
        print(exitSuspended);
        ResetColor();
    }

    void OnMouseEnter() { 
        renderer.material = materialSelected;
    }

    void OnMouseExit() {
        if (!exitSuspended) {
            renderer.material = materialOriginal;
        }
    }

    public void ResetColor() {
        if(exitSuspended)
            renderer.material = materialSelected;
        else 
            renderer.material = materialOriginal;
    }
}
