using UnityEngine;
using System.Collections;

public class ShowHideTest : MonoBehaviour {

    public UISlicedSprite towerWindow;
    public bool isVisible;

    public void ToggleWindow(GameObject btn) {

        isVisible = !isVisible;
        NGUITools.SetActive(towerWindow.gameObject, isVisible);
    }
}
