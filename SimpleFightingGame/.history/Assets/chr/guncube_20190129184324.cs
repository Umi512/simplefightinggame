using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guncube : MonoBehaviour
{
    private CharacterController2 cc;
    private characterController1 cc1;
    // Use this for initialization
    void Start()
    {
        cc = GameObject.Find("player1").GetComponent<CharacterController2>();
        cc1 = GameObject.Find("player2").GetComponent<characterController1>();
    }

    // Update is called once per frame
    void Update()
    {
    }

private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "hand")
        {
            cc.getagun();
            Destroy(gameObject);
        }
        if (col.tag == "hand1")
        {
            cc1.getagun();
            Destroy(gameObject);
        }
    }
}
