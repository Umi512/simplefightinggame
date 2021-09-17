using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIResultController : MonoBehaviour 
{	
    private GameObject resultText;
	///////////////kill,death系///////////////
	private static GameObject[] pText = new GameObject[UIFightingController.PlayerNum];
	private static GameObject[] killText = new GameObject[UIFightingController.PlayerNum];
	private static GameObject[] deathText = new GameObject[UIFightingController.PlayerNum];
    private static GameObject[] pointText = new GameObject[UIFightingController.PlayerNum];
    private int[] point = new int[UIFightingController.PlayerNum];

	private void Start () 
	{
        CalculatePoint();
		Finder();
		Display();
	}

    //勝敗を決めるポイントの計算(kill-death)
    private void CalculatePoint()
    {
        for(int i=0;i<UIFightingController.PlayerNum;i++)
            point[i] = UIFightingController.kill[i]-UIFightingController.death[i];
    }

	//オブジェクトFindまとめ
	private void Finder()
	{
        resultText = GameObject.Find ("Result");

		for(int i=0;i<UIFightingController.PlayerNum;i++)
		{
			pText[i] = GameObject.Find (UIFightingController.killDeathTextName[i,0]);
			killText[i] = GameObject.Find (UIFightingController.killDeathTextName[i,1]);
	 		deathText[i] = GameObject.Find (UIFightingController.killDeathTextName[i,2]);
            pointText[i] = GameObject.Find (UIFightingController.killDeathTextName[i,3]);
		}
	} 

	//一部除くテキスト表示まとめ
	public void Display()
	{
        resultText.GetComponent<Text> ().text = ("Result");

		for(int i=0;i<UIFightingController.PlayerNum;i++)
		{
			pText[i].GetComponent<Text> ().text = ("P"+(i+1));
			killText[i].GetComponent<Text> ().text = (UIFightingController.kill[i].ToString("D2"));
			deathText[i].GetComponent<Text> ().text = (UIFightingController.death[i].ToString("D2"));
            pointText[i].GetComponent<Text> ().text = (point[i].ToString("D2"));
		}
	}
}