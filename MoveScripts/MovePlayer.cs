using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour {

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

    public string color;

    bool jump;
    bool grounded;

    BallDirection ballDirect;
   // Dictionary<Material, string> playerColor= new Dictionary<Material, string>();
    public Material red;
    public Material green;
    public Material blue;

    Vector3 startPos;

    MeshRenderer playerMesh;
   // public Material[] aMaterials;
    void Start() {
        ballDirect =new BallDirection();
        ballDirect = BallDirection.forward;
        //playerColor.Add(red,Colors.Red);
        //playerColor.Add(green,Colors.Green);
        //playerColor.Add(blue,Colors.Blue);

        playerMesh = GetComponent<MeshRenderer>();
        playerTrans = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        _charcontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        playerTrans.Translate(Vector3.forward * _speed * Time.deltaTime);

       // _charcontroller.Move(Vector3.zero);
        // Debug.Log(_charcontroller.isGrounded);

      //  Debug.Log(Input.acceleration.x);
        acceleration.text = Input.acceleration.x.ToString();
        if ((grounded && jump == true)){ // ||  (grounded == true && Input.GetKeyDown(KeyCode.Space))) {

            Debug.Log("jump");
            playerRigidBody.AddForce(Vector3.up*_jumpForce,ForceMode.Impulse);
           // Time.timeScale = 0.5f;
            grounded = false;

            ChangeColor();
            startPos = playerTrans.position;
            ballDirect = BallDirection.forward;
           
        }

        if (Input.GetAxis("Horizontal")<-0.5f && jump==true || Input.acceleration.x<-0.15f && jump==true) {
            jump = false;
           // Time.timeScale = 0.5f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(playerTrans.position.x-1,1f);
            ballDirect = BallDirection.left;
        }

        if (Input.GetAxis("Horizontal") > 0.5f && jump==true || Input.acceleration.x > 0.15f && jump == true) {
            jump = false;
           // Time.timeScale = 0.5f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(playerTrans.position.x + 1, 1f);
            ballDirect = BallDirection.right;
        }

    }

    public void Jump() {

       // Debug.Log("JumpButton");
        if (grounded == false) {
            return;
        }
        //if (_charcontroller.isGrounded==false) {
        //    return;
        //}
        jump = true;

    }

    void ChangeColor() {
        int rand = Random.Range(1, 4);

        switch (rand) {
            case 1:
                playerMesh.material = red;
                color = Colors.Red;
                break;
            case 2:
                playerMesh.material = green;
                color = Colors.Green;
                break;
            case 3:
                playerMesh.material = blue;
                color = Colors.Blue;
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
        if (grounded) {
            return;
        }
        var ball = Instantiate(PaintBall,playerTrans.position,Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Red,ballDirect,startPos);
      //  ball.ChangeColor(Colors.Red);
    }

    public void GreenButton() {
        if (grounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Green,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void BlueButton() {
        if (grounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(Colors.Blue,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void Startagain() {

        SceneManager.LoadScene("1");
    }
}
