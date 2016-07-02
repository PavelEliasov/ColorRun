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
    public Material black;
    public Material yellow;

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

        if (_platformMeshRend.material.color.b <0.1f && _platformMeshRend.material.color.g<0.1f &&
            _platformMeshRend.material.color.r<0.1f) {

            colorOfPlatform = Colors.Black;

        }
        if ( _platformMeshRend.material.color.g - _platformMeshRend.material.color.r < 0.2f) {

            colorOfPlatform = Colors.Yellow;

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
            case Colors.Black:
                _platformMeshRend.material = black;
                colorOfPlatform = Colors.Black;
                break;
            case Colors.Yellow:
                _platformMeshRend.material = yellow;
                colorOfPlatform = Colors.Yellow;
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
