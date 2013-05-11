using UnityEngine;
using System.Collections;

public class LevelGUI : MonoBehaviour {

    public GUISkin mySkin;

    void OnGUI() {
        GUI.skin = mySkin;

        // Calculate best size for Title label font size & button font size
        GUI.skin.button.fontSize = Screen.width / 60;

        GUILayout.BeginArea(new Rect(10, 10, 100, Screen.height-20));
        GUILayout.Button("Missile Tower");
        GUILayout.Button("Laser Tower");
        GUILayout.EndArea();
    }

    void Update() {
        
    }
}
