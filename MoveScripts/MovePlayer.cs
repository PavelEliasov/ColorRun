using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovePlayer : MonoBehaviour {
    public enum Pallette {
        RGB,
        RYB,//Red Yellow Black
        Yellow_Black
    }
    // Use this for initialization
    // public LayerMask whatIsGround;
    public GameObject Smoke;
    public Text acceleration;
    Transform playerTrans;
    Rigidbody playerRigidBody;
    CharacterController _charcontroller;
    Animator animator;

    [SerializeField]
    GameObject PaintBall;
  
   

    public float _speed;
    public float _jumpForce;
    public float _gravity;
    float _verticalSpeed;
    [HideInInspector]
    public string color;

    bool jump;
    bool grounded;

    [HideInInspector]
    public BallDirection ballDirect;

    public Pallette _pallette=Pallette.RGB;
    // Dictionary<Material, string> playerColor= new Dictionary<Material, string>();
   // public SkinnedMeshRenderer playerSkinnedMesh;

    public Material _CharacterMaterial;
    public Material _SmokeMaterial;
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
        Smoke.SetActive(false);
       // DustParticle.SetActive(false);
       // _dustMaterial=


      //  Debug.Log(_SmokeMaterial.color);
        ballDirect = BallDirection.forward;

        animator = GetComponent<Animator>();
        //  _material.SetColor("_Color",red.color);
       // _material = playerSkinnedMesh.material;
        playerMesh = GetComponent<MeshRenderer>();
        playerTrans = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();
        _charcontroller = GetComponent<CharacterController>();

       // Debug.Log(_charcontroller.detectCollisions);

    }

    // Update is called once per frame
    void Update() {
     //   StateManager.playerPos = playerTrans.position;

       // Debug.Log(StateManager.playerPos);
        //playerTrans.Translate(Vector3.forward * _speed * Time.deltaTime);
       
       // _charcontroller.Move(Vector3.zero);
        // Debug.Log(_charcontroller.isGrounded);

      //  Debug.Log(Input.acceleration.x);
        acceleration.text = Input.acceleration.x.ToString();
        
        if ((_charcontroller.isGrounded && jump == true)) { // ||  (grounded == true && Input.GetKeyDown(KeyCode.Space))) {

            //  Debug.Log("jump");
            // playerRigidBody.AddForce(Vector3.up*_jumpForce,ForceMode.Impulse);
            animator.SetBool("Jump",true);
          //  DustParticle.SetActive(false);
            Smoke.SetActive(true);
            Time.timeScale = 0.7f;
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
          
            playerTrans.transform.DOMoveX(Mathf.Round( playerTrans.position.x-1),1f);
            ballDirect = BallDirection.left;
        }

        if (Input.GetAxis("Horizontal") > 0.5f && jump==true || Input.acceleration.x > 0.15f && jump == true) {
            jump = false;
            Time.timeScale = 0.7f;
            StartCoroutine(ReturnTimeScale());
            playerTrans.transform.DOMoveX(Mathf.Round(playerTrans.position.x + 1), 1f);
            ballDirect = BallDirection.right;
        }

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

        if (playerTrans.position.y>=2.5f) {
            Smoke.SetActive(false);
           // Debug.Log("Stop Smoke");
        }

        
        if (_charcontroller.isGrounded) {
            animator.SetBool("Jump",false);
            Smoke.SetActive(false);
            jump = false;
        }
    //   Debug.Log(ballDirect);

    }

    void OnCollisionEnter(Collision other) {
      
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
        if (_pallette==Pallette.Yellow_Black) {
            rand = Random.Range(4, 6);
        }
       

        switch (rand) {
          
            case 1:
                playerMesh.material = green;
                _CharacterMaterial.SetColor("_Color", green.color);
                _SmokeMaterial.SetColor("_TintColor", green.color);
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Green;
                break;
            case 2:
                playerMesh.material = blue;
                _CharacterMaterial.SetColor("_Color", blue.color);
                _SmokeMaterial.SetColor("_TintColor", blue.color);
               
             //   Debug.Log(_SmokeMaterial.color);
                color = Colors.Blue;

                break;
            case 3:
                playerMesh.material = red;
                _CharacterMaterial.SetColor("_Color", red.color);
                _SmokeMaterial.SetColor("_TintColor", red.color);
                
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Red;
                break;
            case 4:
                playerMesh.material = black;
                _CharacterMaterial.SetColor("_Color", black.color);
                _SmokeMaterial.SetColor("_TintColor", Color.grey);
              
            //    Debug.Log(_SmokeMaterial.color);
                color = Colors.Black;
                break;
            case 5:
                playerMesh.material = yellow;
                _CharacterMaterial.SetColor("_Color", yellow.color);
                _SmokeMaterial.SetColor("_TintColor", yellow.color);
               
              //  Debug.Log(_SmokeMaterial.color);
                color = Colors.Yellow;
                break;

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
        ball.GetComponent<PaintBall>().ChangeColor(red.color,Colors.Red,ballDirect,startPos);
      //  ball.ChangeColor(Colors.Red);
    }

    public void GreenButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(green.color,Colors.Green,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void BlueButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(blue.color,Colors.Blue,ballDirect,startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void BlackButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(black.color,Colors.Black, ballDirect, startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void YellowButton() {
        if (_charcontroller.isGrounded) {
            return;
        }
        var ball = Instantiate(PaintBall, playerTrans.position, Quaternion.identity) as GameObject;
        ball.GetComponent<PaintBall>().ChangeColor(yellow.color,Colors.Yellow, ballDirect, startPos);
        //  ball.ChangeColor(Colors.Red);
    }

    public void Startagain() {
        // System.GC.Collect();
      //  StateManager.playerPos = Vector3.zero;

      //  Debug.Log(StateManager.playerPos);
        SceneManager.LoadScene("1");
    }
}
