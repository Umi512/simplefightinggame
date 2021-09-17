using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIFightingController : MonoBehaviour 
{	
	///////////////時間系///////////////
	private float Timer;  //戦闘時間
	private static float time;
	private int second;
	private int minute;
	private GameObject timeText;

	[SerializeField] private int StartCount=5;//戦闘開始待ち時間
	public static float startCount;
	private int startSecond;
	[SerializeField] private int FinishCount=5;//戦闘終了後待ち時間
	private float finishCount;
	private GameObject startFinishText;
	///////////////kill,death系///////////////
	public static int PlayerNum = 2;//Player数
	public static int[] kill = new int[PlayerNum];
	public static int[] death = new int[PlayerNum];
	private GameObject[] pText = new GameObject[PlayerNum];
	private static GameObject[] killText = new GameObject[PlayerNum];
	private static GameObject[] deathText = new GameObject[PlayerNum];
	public static string[,] killDeathTextName = 
	{{"Name1","Kill1","Death1","Point1"},{"Name2","Kill2","Death2","Point2"}};//p,kill,deathText,pointTextの名前

	private void Start () 
	{
		//selectで入力された分秒の反映
		Timer = UISelectController.minute*60 + UISelectController.second;
		//１秒のズレを合わせる
		time= Timer+1;
		startCount = StartCount+1;
		finishCount = FinishCount+1;
		//kill数,death数の初期化
		for(int i=0;i<PlayerNum;i++)
		{
			kill[i]=0;
			death[i]=0;
		}
		Finder();
		Display();
		TimeCount();
	}

	private void Update ()
	{
		if((int)startCount!=0)
			Starting();
		else
		{
			if ((int)time != 0)
				TimeCount();
			else
				Finish();
		}
		//「q」で戦闘強制終了
		if(Input.GetKeyDown(KeyCode.Q))
			SceneManager.LoadScene("Result");
	}

	//オブジェクトFindまとめ
	private void Finder()
	{
		///////////////時間系///////////////
		timeText = GameObject.Find ("Time");
		startFinishText = GameObject.Find ("StartFinish");
		///////////////kill,death系///////////////
		for(int i=0;i<PlayerNum;i++)
		{
			pText[i] = GameObject.Find (killDeathTextName[i,0]);
			killText[i] = GameObject.Find (killDeathTextName[i,1]);
	 		deathText[i] = GameObject.Find (killDeathTextName[i,2]);
		}
	} 

	//一部除くテキスト表示まとめ
	public void Display()
	{
		for(int i=0;i<PlayerNum;i++)
		{
			pText[i].GetComponent<Text> ().text = ("P"+(i+1));
			killText[i].GetComponent<Text> ().text = (kill[i].ToString("D2"));
			deathText[i].GetComponent<Text> ().text = (death[i].ToString("D2"));
		}
	}

	//戦闘開始
	private void Starting()
	{
		//wait 5 second
		startCount -= Time.deltaTime;
		startSecond = (int)startCount;
		startFinishText.GetComponent<Text>().text = startSecond.ToString("D1");

		if((int)startCount==0)
			startFinishText.GetComponent<Text>().text = "Start";
	}

	//時間計算
	private void TimeCount()
	{
		time -= Time.deltaTime;
		minute = (int)time/60;
		second = (int)time%60;
		timeText.GetComponent<Text> ().text = (minute.ToString("D2")+":"+second.ToString("D2"));
		//Startの文字を消す
		if(time <= Timer)
			startFinishText.GetComponent<Text>().text = "";
	}

	//戦闘終了
	private void Finish()
	{
		startFinishText.GetComponent<Text>().text = "Finish";
		//wait 5 second
			finishCount -= Time.deltaTime;
			if((int)finishCount==0)
				SceneManager.LoadScene("Result");
	}

	//引数　殺したオブジェクトのTag
	static public void KillCount(string killTag)
	{
		if((int)time>0)
		{
			//何が殺したか
			if(killTag == "Player")//Playerの手、弾など
			{
				kill[0]+=1;
				killText[0].GetComponent<Text> ().text = (kill[0].ToString("D2"));
			}
			else if(killTag == "Player1")//Player1の手、弾など
			{
				kill[1]+=1;
				killText[1].GetComponent<Text> ().text = (kill[1].ToString("D2"));
			}
		}
	}
    public void KillCount(int killplayer)
    {	
		if((int)time>0)
		{
        	kill[killplayer-1] += 1;
        	killText[killplayer-1].GetComponent<Text>().text = (kill[killplayer-1].ToString("D2"));
		}
    }

    //引数　死んだオブジェクトのTag
    static public void DeathCount(string deathTag)
    {
		if((int)time>0)
		{
			//何が死んだか
			if(deathTag == "Player")//Player
			{
				death[0]+=1;
				deathText[0].GetComponent<Text> ().text = (death[0].ToString("D2"));
				//print("Player");
				//print(death[0]);
			}
			else if(deathTag == "Player1")//Player1
			{
				death[1]+=1;
				deathText[1].GetComponent<Text> ().text = (death[1].ToString("D2"));
				//print("Player1");
				//print(death[1]);
			}
		}
    }
	//引数　死んだオブジェクトのTag
    static public void DeathCount(int deathPlayer)
    {
		if((int)time>0)
		{	
        	death[deathPlayer-1] += 1;
        	deathText[deathPlayer-1].GetComponent<Text>().text = (death[deathPlayer-1].ToString("D2"));	
		}
    }
}