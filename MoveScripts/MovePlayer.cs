using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour {
    public enum Pallette {
        RGB,
        RYB
    }
    // Use this for initialization
    // public LayerMask whatIsGround;
    public Text acceleration;
    Transform playerTrans;
    Rigidbody playerRigidBody;
    CharacterController _charcontroller;

    [SerializeField]
    GameObject PaintBall;

    public float _speed;
    public float _jumpForce;
    public float _gravity;
    float _verticalSpeed;

    public string color;

    bool jump;
    bool grounded;

    [HideInInspector]
    public BallDirection ballDirect;

    public Pallette _pallette=Pallette.RGB;
   // Dictionary<Material, string> playerColor= new Dictionary<Material, string>();
    public Material red;
    public Material green;
    public Material blue;
    public Material black;
    public Material yellow;

    Vector3 startPos;
    Vector3 movement;

    MeshRenderer playerMesh;
   // public Material[] aMaterials;
    void Start() {
        ballDirect = BallDirection.forward;
    

        playerMesh = GetComponent<MeshRenderer>();
        playerTrans = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        _charcontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        //playerTrans.Translate(Vector3.forward * _speed * Time.deltaTime);
       
       // _charcontroller.Move(Vector3.zero);
        // Debug.Log(_charcontroller.isGrounded);

      //  Debug.Log(Input.acceleration.x);
        acceleration.text = Input.acceleration.x.ToString();
        
        if ((_charcontroller.isGrounded && jump == true)) { // ||  (grounded == true && Input.GetKeyDown(KeyCode.Space))) {

            Debug.Log("jump");
            // playerRigidBody.AddForce(Vector3.up*_jumpForce,ForceMode.Impulse);
            Time.timeScale = 0.9f;
            StartCoroutine(ReturnTimeScale());
            grounded = false;

            ChangeColor();
            startPos = playerTrans.position;
            ballDirect = BallDirection.forward;

            _verticalSpeed = _jumpForce;


        }
    

        if (Input.GetAxis("Horizontal")<-0.5f && jump==true || Input.acceleration.x<-0.15f && jump==true) {
            jump = false;
            Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(playerTrans.position.x-1,1f);
            ballDirect = BallDirection.left;
        }

        if (Input.GetAxis("Horizontal") > 0.5f && jump==true || Input.acceleration.x > 0.15f && jump == true) {
            jump = false;
            Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(playerTrans.position.x + 1, 1f);
            ballDirect = BallDirection.right;
        }

        //if (_charcontroller.isGrounded && grounded == false) {
        //  //  jump = false;
        //    grounded = true;
        //}

        if (_charcontroller.isGrounded == false) {
            _verticalSpeed += _gravity * 2 * Time.deltaTime;
            if (_verticalSpeed <= _gravity) {
                _verticalSpeed = _gravity;
            }
           // Debug.Log(_verticalSpeed);
        } 

       
        movement = new Vector3(0,0, _speed);
        movement.y = _verticalSpeed;
        movement *= Time.deltaTime;
        _charcontroller.Move(movement);

        if (_charcontroller.isGrounded) {
            jump = false;
        }
    //   Debug.Log(ballDirect);

    }


    
    public void Jump() {

        // Debug.Log("JumpButton");
        //if (grounded == false) {
        //    return;
        //}
        if (_charcontroller.isGrounded == false) {
            return;
        }
        jump = true;

    }

    void ChangeColor() {
        int rand = Random.Range(1, 4);

        if (_pallette==Pallette.RGB) {
             rand = Random.Range(1, 4);
        }
        if (_pallette == Pallette.RYB) {
             rand = Random.Range(3, 6);
        }
       

        switch (rand) {
          
            case 1:
                playerMesh.material = green;
                color = Colors.Green;
                break;
            case 2:
                playerMesh.material = blue;
                color = Colors.Blue;
                break;
            case 3:
                playerMesh.material = red;
                color = Colors.Red;
                break;
            case 4:
                playerMesh.material = black;
                color = Colors.Black;
                break;
            case 5:
                playerMesh.material = yellow;
                color = Colors.Yellow;
                break;

        }

    }

    void OnCollisionEnter(Collision other) {

        Debug.Log("Collision");
        if (other.gameObject.tag=="Ground") {
            grounded = true;
            jump = false;
          //  Debug.Log("Ground");
        }
        else {
           // grounded = false;
        }

    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag=="Ground") {
            grounded = false;
        }
    }

    IEnumerator ReturnTimeScale() {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;

    }

    public void RedButton() {

        Debug.Log("Red");
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall,playerTrans.position,Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Red,ballDirect,startPos);
      //  ball.ChangeColor(Colors.Red);
    }

    public void GreenButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Green,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void BlueButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Blue,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void BlackButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Black, ballDirect, startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void YellowButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Yellow, ballDirect, startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void Startagain() {

        SceneManager.LoadScene("1");
    }
}
