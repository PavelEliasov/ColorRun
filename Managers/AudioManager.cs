using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour,IGameManager {
    public ManagerStatus status  { get; set; }

    public void Startup() {
        status = ManagerStatus.Started;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
