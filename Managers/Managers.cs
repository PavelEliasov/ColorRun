using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour {
    public static AudioManager _audioManager { get; private set; }

    List<IGameManager> managers;
    // Use this for initialization

    void Awake() {
        managers = new List<IGameManager>();
        _audioManager = GetComponent<AudioManager>();

       // Debug.Log(_audioManager);

        managers.Add(_audioManager);
        StartCoroutine(StartUpManagers());
    }

    IEnumerator StartUpManagers() {
        foreach (IGameManager manager in managers) {
            manager.Startup();
        }
        yield return null;

        int startedManagerscount = 0;

        while (startedManagerscount<managers.Count) {
            foreach (IGameManager manager in managers) {
                if (manager.status==ManagerStatus.Started) {
                    startedManagerscount++;
                }
            }
            
        }
        Debug.Log(startedManagerscount+"/"+managers.Count + " managers started");

    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
