using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBlockController : MonoBehaviour {

	[SerializeField] private int powerX=0;
	[SerializeField] private int powerY=500;
	private Vector3 target;
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.CompareTag("Player")||coll.CompareTag("Player1"))
		{	//Playerの速度に合わせて、ジャンプ力調整
			target = -coll.GetComponent<Rigidbody2D>().velocity;
			coll.GetComponent<Rigidbody2D>().AddForce (new Vector3(powerX,powerY,0)+target*50);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
