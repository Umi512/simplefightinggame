using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletController : MonoBehaviour {

	[SerializeField] private float bullet_speed = 1.0f;

	[SerializeField] private int loop=1; //bulletのループ回数
    GameObject leftLoop;
    GameObject rightLoop;
	private float worpCoolTime;
    
	private void Start ()
	{
		worpCoolTime = 0.5f;
        leftLoop = GameObject.Find ("LoopLeft");
		rightLoop = GameObject.Find ("LoopRight");
		
		worpCoolTime += Time.deltaTime;
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
		if(loop>0)
		{print("warp cool time");
        	if (coll.gameObject.tag == "LoopLeft" && worpCoolTime > 0.5) {
				worpCoolTime = 0;
				Vector3 pos = this.gameObject.transform.position;
				this.gameObject.transform.position = new Vector3 (rightLoop.transform.position.x, pos.y, pos.z);
				print ("into leftroop");
				loop--;
				//print("tasss111");
			}

			else if (coll.gameObject.tag == "LoopRight" && worpCoolTime > 0.5) {
				print ("into rightroop");
				worpCoolTime = 0;
				Vector3 pos = this.gameObject.transform.position;
				this.gameObject.transform.position = new Vector3 (leftLoop.transform.position.x, pos.y, pos.z);
				loop--;
				//print("tasss");
			}
			else
				Destroy (gameObject);
		}
		else
			Destroy (gameObject);
		print(loop);
	}
}