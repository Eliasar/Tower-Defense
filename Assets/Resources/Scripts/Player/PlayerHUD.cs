using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

    public GUISkin mySkin;
    //public GUIStyle titleStyle; // Custom style changed in the editor
    //public Player player;
    public Game game;

    void Start() {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnGUI() {
        GUI.skin = mySkin;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));
        GUILayout.BeginHorizontal();
            GUILayout.Label("Level: " /*+ game.currentLevel*/);
            GUILayout.Label("Cash: " /*+ cash */);
        GUILayout.EndHorizontal();
        GUILayout.Label("Monsters Left: " /*+ player.livesLeft*/);
        GUILayout.EndArea();
    }
}
