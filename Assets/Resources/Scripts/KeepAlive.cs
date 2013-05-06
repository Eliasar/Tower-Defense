using UnityEngine;
using System.Collections;

public class KeepAlive : MonoBehaviour {

    void Awake() { DontDestroyOnLoad(gameObject); }
}
