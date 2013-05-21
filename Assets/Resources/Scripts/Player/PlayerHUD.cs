using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

    public GUISkin mySkin;
    public Game game;

    void Start() {
    }

    public void OnGUI() {
        GUI.skin = mySkin;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));
        GUILayout.Label("Wave: " /*+ game.currentLevel*/);
        GUILayout.Label("Cash: " /*+ cash */);
        GUILayout.Label("Monsters Left: " /*+ player.livesLeft*/);
        GUILayout.EndArea();
    }
}
