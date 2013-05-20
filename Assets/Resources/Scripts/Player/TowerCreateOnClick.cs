using UnityEngine;
using System.Collections;

public class TowerCreateOnClick : MonoBehaviour {

    public GameObject uiPrefab;

    void OnMouseDown() {
        print("Clicked on the tower!");
        //NGUITools.AddChild(transform.parent.gameObject, uiPrefab);
    }
}
