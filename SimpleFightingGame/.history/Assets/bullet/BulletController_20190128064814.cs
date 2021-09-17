using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	[SerializeField] private float bullet_speed = 1.0f;


	public GameObject explosionPrefab;
    //private int key = characterController.key ;
        
    

    private void Update () {
		transform.Translate (bullet_speed ,0 , 0);
        //print(key);
		if (Mathf.Abs(transform.position.x) > 50) {
			Destroy (gameObject);
		}
	}

    private void OnTriggerEnter2D(Collider2D coll) {
		// 衝突したときにスコアを更新する
		//GameObject.Find ("Canvas").GetComponent<UIController> ().AddScore ();

		// 爆発エフェクトを生成する	
		//GameObject effect = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		//Destroy (effect, 1.0f);
        
		//Destroy (coll.gameObject);	
		Destroy (gameObject);
	}
}
