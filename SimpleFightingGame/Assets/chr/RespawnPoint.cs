using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {




    public GameObject RespawnPointZone;
    private float res_posx;
    private float res_posy;
    private Vector3 zonePos;
    private Vector3 zoneScale;
    // Use this for initialization
    void Start () {
        RespawnPointZone = GameObject.Find("RespawnPointZone");
        print("respown");
        zonePos = RespawnPointZone.transform.position;
        zoneScale = RespawnPointZone.transform.localScale;
        res_posx = (zonePos.x - zoneScale.x / 2);
        res_posy = zonePos.y ;

    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = new Vector3 (res_posx + Random.Range(0,zoneScale.x), res_posy,0);
    }
}
