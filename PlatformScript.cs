using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
    public enum PlatformColor {
        red,
        green,
        blue
    }
    PlatformColor _platformColor;

    MeshRenderer _platformMeshRend;

    string colorOfPlatform;
    
    public Material red;
    public Material green;
    public Material blue;

    MovePlayer player;
    // Use this for initialization
    void Start () {
        _platformMeshRend = GetComponent<MeshRenderer>();
        if (FindObjectOfType<MovePlayer>() !=null) {
            player = FindObjectOfType<MovePlayer>();
        }
        

        if (_platformMeshRend.material.color.r> _platformMeshRend.material.color.g && 
            _platformMeshRend.material.color.r > _platformMeshRend.material.color.b ) {

            colorOfPlatform = Colors.Red;

        }

        if (_platformMeshRend.material.color.g > _platformMeshRend.material.color.r &&
          _platformMeshRend.material.color.g > _platformMeshRend.material.color.b) {

           colorOfPlatform = Colors.Green;

        }

        if (_platformMeshRend.material.color.b > _platformMeshRend.material.color.g &&
          _platformMeshRend.material.color.b > _platformMeshRend.material.color.r) {

           colorOfPlatform = Colors.Blue;

        }
       // Debug.Log(_platformMeshRend.material.color.r);
	
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(player.gameObject.tag);
	}

    //void StartColor(PlatformColor color) {
    //    switch (color) {
    //        case PlatformColor.red:
    //            _platformMeshRend.material = red;
    //            colorOfPlatform = Colors.Red;
    //            break;
    //        case PlatformColor.green:
    //            _platformMeshRend.material = green;
    //            colorOfPlatform = Colors.Green;
    //            break;
    //        case PlatformColor.blue:
    //            _platformMeshRend.material = blue;
    //            colorOfPlatform = Colors.Blue;
    //            break;
    //    }

    //}

    void ChangePlatformColor(string color) {
        switch (color) {
            case Colors.Red:
                _platformMeshRend.material = red;
                colorOfPlatform = Colors.Red;
                break;
            case Colors.Green:
                _platformMeshRend.material = green;
                colorOfPlatform = Colors.Green;
                break;
            case Colors.Blue:
                _platformMeshRend.material = blue;
                colorOfPlatform = Colors.Blue;
                break;
        }

    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag=="Player") {
            if (player.color == colorOfPlatform) {

                Debug.Log("Equal Of Colors");
            }
            else {

                Debug.Log("Colors not Equal");
            }
        }
      

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PaintBall") {

            ChangePlatformColor(other.gameObject.GetComponent<PaintBall>().color);
        }
    }
}
