using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBlockController : MonoBehaviour 
{       
    [SerializeField] private int RebornCount=5;//復活にかかる時間
    private float rebornCount;
    private bool rebornSet;
	private Vector3 startPosition;
	
	private void Start () 
	{
        rebornSet = true;
        rebornCount=RebornCount+1;
        //ブロックの初期位置を登録 
		startPosition=transform.position;
        
	}

	private void Update () 
	{
        if(rebornSet==false)
            Respawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Bullet")
        {
            rebornSet = false;
            transform.position=transform.position+new Vector3(0,50f,0);
        }
    }

    //時間が経ったらブロック復活
    private void Respawn()
    {
        rebornCount-=Time.deltaTime;//秒ごとにカウント
        
        if((int)rebornCount==0)
            {
                transform.position=startPosition;
                rebornSet=true;   
                rebornCount=RebornCount+1;
            }
    }
}