﻿using System.Collections;using System.Collections.Generic;using UnityEngine;public class handController : MonoBehaviour{    private CharacterController2 cc;    // Use this for initialization    void Start()    {        cc = GameObject.Find("player1").GetComponent<CharacterController2>();    }    // Update is called once per frame    void Update()    {    }    void OnTriggerEnter2D(Collider2D coll)    {            //print("attack");            //print(coll.gameObject.tag);            if (coll.gameObject.tag == "attackerPlayer")            {            print(coll);            cc.getagun();            //GameObject.Find("playercon").GetComponent<playerController>().getagunplayer("player");            }       }}