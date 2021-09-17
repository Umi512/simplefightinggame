using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class handController1 : MonoBehaviour
{
    private characterController1 cc;
    // Use this for initialization
    void Start()
    {
        cc = GameObject.Find("player2").GetComponent<characterController1>();
    }

    // Update is called once per frame
    void Update()
    {
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
