using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour 
{
	//public GameObject respawnPoint;
	public GameObject bulletPrefab;   //equip bullet
	private Rigidbody2D rb2d;                 //this is variable to get component Rigidbody2D
	[SerializeField] private const int MAX_JUMP = 2;           //air jump = MAX_JUMP - 1
	private int j_count;           //jump counter  
	private bool jump;                //on the floor or not (true/false)
	public static Vector3 startPosition;

	
	//if player hit floor
	void OnTriggerEnter2D(Collider2D col)
	{
		//ground接地処理
		if(col.tag=="Ground"||col.tag == "TransformGround")
		{
			this.jump = true;
			this.j_count = MAX_JUMP;
			this.rb2d.AddForce(Vector2.zero * 300f);//don't through ceiling
		}

		//キャラクター復活 自分で打ったBulletでは死なない
		if(col.tag=="hand"||col.tag=="DeadLine"||col.tag=="Bullet")
		{
			/* 
			//Playerの初期位置のどちらかに復活
			if((int)Random.Range(0.0f, 100.0f)%2==0)
				this.transform.position=Human.startPosition;
			else
				this.transform.position=Human_1.startPosition;
			*/
			//どれかのRespawnPointに復活
			if((int)Random.Range(0.0f, 100.0f)%5==0)
				this.transform.position=GameObject.Find("RespawnPoint").transform.position;
			else if((int)Random.Range(0.0f, 100.0f)%5==1)
				this.transform.position=GameObject.Find("RespawnPoint (1)").transform.position;
			else if((int)Random.Range(0.0f, 100.0f)%5==2)
				this.transform.position=GameObject.Find("RespawnPoint (2)").transform.position;
			else if((int)Random.Range(0.0f, 100.0f)%5==3)
				this.transform.position=GameObject.Find("RespawnPoint (3)").transform.position;
			else
				this.transform.position=GameObject.Find("RespawnPoint (4)").transform.position;
		}

		//キャラクターを動く床の動きに追従させる
		if (col.tag == "TransformGround")
        	transform.SetParent(col.transform);
	}

	//if player left floor
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag=="Ground"||col.tag == "TransformGround")
			this.jump = false;
	
		//キャラクターを動く床の動きに追従させるのをやめる	
		if (col.tag == "TransformGround") 
        	transform.SetParent(null);
	}
		
	void Start () 
	{ 
		j_count = MAX_JUMP;
		jump = false;
		startPosition=transform.position;
	
		this.rb2d = GetComponent<Rigidbody2D>(); //get component Rigidbody2D
	}

	void Update () 
	{
		if((int)UIFightingController.startCount==0)
		{
			//move left
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.rb2d.AddForce (Vector2.left * 10f); //add "facing left vector power" to player to move it left
			}
			//move right
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.rb2d.AddForce(Vector2.right * 10f); //add "facing right vector power" to player to move it right
			}
			//leave floor && no jump
			if (jump == false && j_count == MAX_JUMP)
				j_count--;
			//jump
			if ((Input.GetKeyDown(KeyCode.UpArrow))&&(j_count>0)) {
				rb2d.AddForce(Vector2.up * 300f);  //add "facing above vector power" to player to make it jump
				j_count--;
			}
			//shot bullet
			if (Input.GetKeyDown (KeyCode.Space)) {
				Instantiate (bulletPrefab, transform.position, Quaternion.identity);
			}
		}
	}
}
