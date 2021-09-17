using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class ButtonScript : MonoBehaviour 
{
	/* 
	public GameObject bulletPrefab;
	GameObject rocket;
	bool L_OnState = true;
	bool R_OnState = true;

	public void LeftPush() {
		L_OnState = false;
	}
	public void LeftPull() {
		L_OnState = true;
	}
	public void RightPush() {
		R_OnState = false;
	}
	public void RightPull() {
		R_OnState = true;
	}
	public void BulletPush() {
		Instantiate (bulletPrefab, GameObject.Find ("rocket").transform.position, Quaternion.identity);;
	}
	*/
	public void SelectPush() {
		SceneManager.LoadScene("Select");
	}
	public void FightingPush() {
        print(UISelectController.FightingScene);
		SceneManager.LoadScene(UISelectController.FightingScene);
	}
	
	public void QuitPush() {
		//条件付きコンパイル
		//quit in case editor
		//#if UNITY_EDITOR
		//EditorApplication.isPlaying = false;
		//quit in case Application
		//#elif UNITY_STANDALONE
		Application.Quit();
		//#endif
	}

	public void TitlePush() {
		SceneManager.LoadScene("Title");
	}
	
	void Start() {
		//rocket = GameObject.Find ("rocket"); //rocket Object into rocket C# GameObject
	}

	// Update is called once per frame
	void Update () {
		/* 
		if(L_OnState==false){
			rocket.GetComponent<RocketController> ().transform.Translate (-0.1f, 0, 0);;
		}
		if(R_OnState==false){
			rocket.GetComponent<RocketController> ().transform.Translate (0.1f, 0, 0);;
		}
		*/			
	}
}