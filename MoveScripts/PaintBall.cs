﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PaintBall : MonoBehaviour {
    public string color;

    public Material red;
    public Material green;
    public Material blue;

    MeshRenderer paintBallMeshRend;
    Transform paintBallTransform;

    BallDirection fallingDirection;
    // Use this for initialization
    void Awake() {

        paintBallMeshRend = GetComponent<MeshRenderer>();
        paintBallTransform = GetComponent<Transform>();
        Debug.Log("Awake");
    }

    void Start () {
      

 }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void ChangeColor(string color,BallDirection direct,Vector3 startpos) {
        // fallingDirection = direct;
       // Vector3 startpos = paintBallTransform.localPosition;

        Debug.Log(startpos);
        switch (color) {
            case Colors.Red:
                paintBallMeshRend.material = red;
                this.color = Colors.Red;
                break;
            case Colors.Green:
                paintBallMeshRend.material = green;
                this.color = Colors.Green;
                break;
            case Colors.Blue:
                paintBallMeshRend.material = blue;
                this.color = Colors.Blue;
                break;
        }

        switch (direct) {
            case BallDirection.left:
                paintBallTransform.DOMoveX(startpos.x-1,0.5f);
               
                break;
            case BallDirection.right:
                paintBallTransform.DOLocalMoveX(startpos.x+1, 0.5f);
                break;
            case BallDirection.forward:
                break;

        }
        paintBallTransform.DOMoveZ(startpos.z + 5, 0.5f);
        paintBallTransform.DOMoveY(startpos.y -3, 1.5f);

    }
}
