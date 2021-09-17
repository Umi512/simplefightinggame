using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UISelectController : MonoBehaviour 
{	
	static public int minute;
	static public int second;
	static public string FightingScene;
    private GameObject timeText;

	private void Start () 
	{
		minute = 1;
		second = 0;
		FightingScene = "SimpleStage";
		Finder();
	}

	private void Update() 
	{
		Display();
	}

	//オブジェクトFindまとめ
	private void Finder()
	{
        	timeText = GameObject.Find ("TimeView");
	} 

	//一部除くテキスト表示まとめ
	public void Display()
	{
        	timeText.GetComponent<Text> ().text = (minute.ToString("D2") +":"+ second.ToString("D2"));
	}
}