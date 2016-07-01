using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    Transform playerTransform;

    Transform cameraTransform;

    public float _distance;
    Vector3 startPos;
    Sequence move;

    // Use this for initialization
    void Start () {
        move = DOTween.Sequence();
       // DOTween.defaultAutoKill = false;
        cameraTransform = GetComponent<Transform>();
        startPos = cameraTransform.localPosition;
        // move.Append(cameraTransform.DOMove(new Vector3(playerTransform.position.x, startPos.y, playerTransform.position.z - _distance),));

        Debug.Log(startPos.x);
	}
	
	// Update is called once per frame
	void Update () {
       
     //  cameraTransform.position = new Vector3(playerTransform.position.x,startPos.y,playerTransform.position.z-_distance);
	}
    void LateUpdate() {
        cameraTransform.LookAt(playerTransform.position + Vector3.up * 0.5f);
    }

   public void MoveBack() {
      
        move.Append(cameraTransform.DOLocalMoveZ(startPos.z - _distance , 1f));
        move.Join(cameraTransform.DOLocalMoveY(startPos.y + _distance, 1f));
        StartCoroutine(AnimateCamera());
       // move.Append(cameraTransform.DOMoveZ(playerTransform.position.z - _distance, 1f));
       // cameraTransform.DOMoveZ(playerTransform.position.z - _distance - 2, 1f);
        //move.PlayBackwards();
        //Debug.Log(move.IsComplete());

    }
    IEnumerator AnimateCamera() {
        yield return new WaitForSeconds(0.6f);
        move.Append(cameraTransform.DOLocalMoveZ(startPos.z, 0.6f));
        move.Join(cameraTransform.DOLocalMoveY(startPos.y, 1f));
    }
}
