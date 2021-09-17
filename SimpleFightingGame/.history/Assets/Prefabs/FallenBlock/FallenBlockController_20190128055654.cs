using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenBlockController : MonoBehaviour 
{       
    //static public string id;
    //static public int hash;
    [SerializeField] private float waitFall=0.5f;
    [SerializeField] private int fallLength=100;
    private Vector3 startPosition;
    private Rigidbody2D rb2d;
    private PlatformEffector2D pe2d;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player")||coll.gameObject.CompareTag("Player1")||coll.gameObject.CompareTag("attackerPlayer"))
        {
            Invoke("Fall",waitFall);//Fall関数を1秒後に実行
        }
	}

     //PlatformEffector2D(collider2D coll)
     //{

     //}

    //重力の影響を受けさせて、落とす
    void Fall()
    {
        this.rb2d.isKinematic = false;
        this.pe2d.surfaceArc = 180;
    } 
    
    //初期位置に再配置
    void Respawn()
    {
        this.transform.position=startPosition;
        this.rb2d.velocity=Vector3.zero;
        this.rb2d.isKinematic = true;
        this.pe2d.surfaceArc = 360;
    }

	void Start () 
	{
        //オレンジブロックの初期位置を登録
        startPosition=transform.position;

	    this.rb2d = GetComponent<Rigidbody2D>(); //get component Rigidbody2D
        this.rb2d.isKinematic = true;
        this.pe2d = GetComponent<PlatformEffector2D>();
        this.pe2d.surfaceArc = 360;//足場を踏んだ時に下から貫通をtrue
	}
	void Update () 
	{
        if(transform.position.y <= startPosition.y-fallLength)
        {
            Respawn();
        }
	}
}
