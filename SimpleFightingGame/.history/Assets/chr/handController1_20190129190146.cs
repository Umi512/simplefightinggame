using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController1 : MonoBehaviour
{
    private characterController1 cc;
    // Use this for initialization
    [SerializeField] private KeyCode attackKey = KeyCode.S;
    private float time;
    private bool t_switch = false; 
    void Start()
    {
        cc = GameObject.Find("player2").GetComponent<characterController1>();
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(attackKey))
        {
            this.tag = "hand1";
            t_switch = true;
        }
        if (t_switch == true)
        {
            time += Time.deltaTime;
        }
        if(time > 1.0)
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
            if (coll.gameObject.tag == "attackerPlayer")
            {
            cc.getagun();
        }
        
    }
}
