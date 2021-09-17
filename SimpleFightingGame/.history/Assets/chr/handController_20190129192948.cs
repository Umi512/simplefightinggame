using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class handController : MonoBehaviour
{
    private CharacterController2 cc;
    [SerializeField] private KeyCode attackKey = KeyCode.DownArrow;
    private float time;
    private bool t_switch;

    // Use this for initialization
    void Start()
    {
        cc = GameObject.Find("player1").GetComponent<CharacterController2>();
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(attackKey))
        {
            this.tag = "hand";
            t_switch = true;
        }
        if (t_switch == true)
        {
            time += Time.deltaTime;
        }
        if(time > 1.5)
        {
            t_switch = false;
            time = 0.0f;
            this.tag = "Untagged";
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
            //print("attack");
            //print(coll.gameObject.tag);
            if (this.tag == "hand" && coll.gameObject.tag == "attackerPlayer")
            {
            print(coll);
            cc.getagun();
            //GameObject.Find("playercon").GetComponent<playerController>().getagunplayer("player");
            }   
    }
}
