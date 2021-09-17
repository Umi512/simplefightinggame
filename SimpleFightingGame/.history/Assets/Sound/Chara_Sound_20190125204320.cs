using UnityEngine;
using System.Collections;

public class Chara_Sound : MonoBehaviour {
    //キーコード https://docs.unity3d.com/ja/550/ScriptReference/KeyCode.html
    private KeyCode jumpkey = KeyCode.UpArrow;
    private KeyCode attackkey = KeyCode.DownArrow;

    public AudioClip audioHand;
    public AudioClip audioBullet;
    public AudioClip audioWalk;
    public AudioClip audioJump;
    public AudioClip audioDeath;
    public AudioClip audioWarp;
    public AudioClip audioJumpingBlock;
    private AudioSource audioSource;
    private CharacterController2 cc;

    void Start () 
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        cc = GameObject.Find("player1").GetComponent<CharacterController2>();
    }

    private void Update()
    {
        /* 
        var input_h = cc.Get_inputs();

        // 左右方向の入力を受け取る
        if(input_h != 0 && cc.state == "RUN")
        {
            Invoke("RunSound",0.2f);
        }
        */
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cc.isGround)
            {
                audioSource.clip = audioJump;
                audioSource.Play ();
            }
        }

		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cc.have_agun && cc.timer >= cc.time_bettween_shot) 
			{
                audioSource.clip = audioBullet;
                audioSource.Play ();
            }
            else if(!cc.have_agun) 
            {
                audioSource.clip = audioHand;
                audioSource.Play ();
            }
		}
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hand" || col.gameObject.tag =="bullet")
        {
            audioSource.clip = audioDeath;
            audioSource.Play ();
        }
		if (col.gameObject.tag == "worpPoint1" && cc.worpCoolTime>0.5)
		{
			cc.worpCoolTime = 0;
			audioSource.clip = audioWarp;
            audioSource.Play ();
		}
		else if (col.gameObject.tag == "worpPoint2"&& cc.worpCoolTime>0.5)
		{
			cc.worpCoolTime = 0;
			audioSource.clip = audioWarp;
            audioSource.Play ();
		}
        if (col.gameObject.tag == "JumpingBlock")
		{
			audioSource.clip = audioJumpingBlock;
            audioSource.Play ();
		}
    }
/* 
    private void RunSound()
    {
        audioSource.clip = audioWalk;
        audioSource.Play ();
    }*/
}