using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletController : MonoBehaviour {

	[SerializeField] private float bullet_speed = 1.0f;

	[SerializeField] private int loop=10; //bulletのループ回数
    private GameObject leftLoop;
    private GameObject rightLoop;
	private float worpCoolTime = 0.5f;
	private GameObject UI;
	static public int who;
    
	private void Start ()
	{
		UI = GameObject.Find("FightingCanvas");
		worpCoolTime = 2.0f;
        leftLoop = GameObject.Find ("LoopLeft");
		rightLoop = GameObject.Find ("LoopRight");
	}

    private void Update () 
	{
		worpCoolTime += Time.deltaTime;

		transform.Translate (bullet_speed ,0 , 0);
        //print(key);
		if (Mathf.Abs(transform.position.x) > 50) {
			Destroy (gameObject);
		}
	}

    private void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player1")
		{
			if(who == 1)
				UI.GetComponent<UIFightingController>().KillCount(1);
			if(who == 2)
				UI.GetComponent<UIFightingController>().KillCount(2);
		}
		if(loop>0)
		{
        	if (coll.gameObject.tag == "LoopLeft" && worpCoolTime > 0.8f) {
				worpCoolTime = 0.0f;
				Vector3 pos = this.gameObject.transform.position;
				this.gameObject.transform.position = new Vector3 (rightLoop.transform.position.x, pos.y, pos.z);
				loop--;
				print("tasss111");
			}

			else if (coll.gameObject.tag == "LoopRight" && worpCoolTime > 0.8f) {
				worpCoolTime = 0.0f;
				Vector3 pos = this.gameObject.transform.position;
				this.gameObject.transform.position = new Vector3 (leftLoop.transform.position.x, pos.y, pos.z);
				loop--;
				print("tasss");
			}
			else if (worpCoolTime < 0.8f)
			{}
			else
			{
				Destroy (gameObject);
				print("meme");
			}
		}
		else
			Destroy (gameObject);
		print(loop);
	}
}