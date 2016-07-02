using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatformScript : MonoBehaviour {
    public enum PlatformColor {
        red,
        green,
        blue
    }
    PlatformColor _platformColor;

    public enum AnimateDirection {
        Default,
        Left,
        Right,
        Backward,
        Forward,
        Up,
        Down
    }

    public AnimateDirection FlyDirection = AnimateDirection.Default;

    MeshRenderer _platformMeshRend;
    Transform _platformTrans;

    Vector3 startPos;

    string colorOfPlatform;
    
    public Material red;
    public Material green;
    public Material blue;
    public Material black;
    public Material yellow;

    MovePlayer player;

    bool hide=true;

    // Use this for initialization
    void Awake() {
       
    }
    void Start () {

        _platformTrans = GetComponent<Transform>();
        startPos = _platformTrans.position;

        _platformMeshRend = GetComponent<MeshRenderer>();
        if (FindObjectOfType<MovePlayer>() !=null) {
            player = FindObjectOfType<MovePlayer>();
        }


        colorOfPlatform = IdentifyColor(_platformMeshRend.material.color);
        _platformMeshRend.enabled = false; 


        // Debug.Log(_platformMeshRend.material.color.r);

    }
	
	// Update is called once per frame
	void Update () {

        if ((_platformTrans.position-StateManager.playerPos).magnitude<15 && hide) {

            hide = false;
            StartMove();
            _platformMeshRend.enabled =true;
            // Debug.Log("Uhide");
        }
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
    string IdentifyColor(Color color) {
        if (color.r >color.g && color.r > color.b) {

            return Colors.Red;

        }

        if (color.g >color.r && color.g > color.b) {

            return Colors.Green;

        }

        if (color.b > color.g && color.b > color.r) {

            return Colors.Blue;

        }

        if (color.b < 0.1f && color.g < 0.1f && color.r < 0.1f) {

            return Colors.Black;

        }
        if (color.g - color.r < 0.2f) {

            return Colors.Yellow;

        }
        return Colors.Yellow;

    }

    void StartMove() {
        switch (FlyDirection) {
            case AnimateDirection.Default:
                
                break;
            case AnimateDirection.Left:
                _platformTrans.position = new Vector3(startPos.x - 15, startPos.y, startPos.z);
                _platformTrans.DOMoveX(startPos.x,1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Right:
                _platformTrans.position = new Vector3(startPos.x + 15, startPos.y, startPos.z);
                _platformTrans.DOMoveX(startPos.x, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Down:
                _platformTrans.position = new Vector3(startPos.x , startPos.y + 15, startPos.z);
                _platformTrans.DOMoveY(startPos.y, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Up:
                _platformTrans.position = new Vector3(startPos.x, startPos.y - 15, startPos.z);
                _platformTrans.DOMoveY(startPos.y, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Backward:
                _platformTrans.position = new Vector3(startPos.x, startPos.y, startPos.z + 15);
                _platformTrans.DOMoveZ(startPos.z, 1f).SetEase(Ease.InOutExpo);
                break;
            case AnimateDirection.Forward:
                _platformTrans.position = new Vector3(startPos.x, startPos.y, startPos.z - 20);
                _platformTrans.DOMoveZ(startPos.z, 1f).SetEase(Ease.InOutExpo);
                break;

        }

    }
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

    //void OnControllerColliderHit(ControllerColliderHit other) {

    //    Debug.Log(other);
    //}
}
