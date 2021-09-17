using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeftRightBlock : MonoBehaviour {

    private Vector3 startPosition;
    private bool vector;
    [SerializeField] private float distance=2; 
    [SerializeField] private float speedX = 0.03f;
    [SerializeField] private float speedY = 0.0f;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
        vector=true;
	}
	
	// Update is called once per frame
	void Update () {
        //初期位置を中心とした左右移動
        if(transform.position.x>=startPosition.x+distance||transform.position.y>=startPosition.y+distance)
            vector=false;
        if(transform.position.x<=startPosition.x-distance||transform.position.y<=startPosition.y-distance)  
            vector=true; 

        if(vector)
	        transform.Translate( speedX, speedY, 0, Space.World);
        else
            transform.Translate(-speedX, -speedY, 0 , Space.World); 
	}
    /* 
　   //キャラクターを動く床の動きに追従させる
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            GameObject.Find("Human").GetComponent<Human>().transform.SetParent(this.transform);
        }
        else if (col.gameObject.tag == "Player1") {
            GameObject.Find("Human1").GetComponent<Human_1>().transform.SetParent(this.transform);
        }
    }
 
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            GameObject.Find("Human").GetComponent<Human>().transform.SetParent(null);
        }
        else if (col.gameObject.tag == "Player1") {
            GameObject.Find("Human1").GetComponent<Human_1>().transform.SetParent(null);
        }
    }
    */
}