using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// https://tech.pjin.jp/blog/2016/09/27/unity_skill_4/
public class InputFieldScript : MonoBehaviour 
{	
	InputField inputField;

	private void Start () 
	{
		inputField = GetComponent<InputField>();
       	//InitInputField();
	}

	private void Update ()
	{
		
	}

	/* 
	/// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする(InputField有効化)	
	private void InitInputField()
	{
    	// 値をリセット
      	inputField.text = "";
        // フォーカス
        inputField.ActivateInputField();
    }

	/// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
	 private string InputLogger() 
	 {
		string input;
        input = inputField.text;
        Debug.Log(input);
        //InitInputField();
		return input;
    }
	*/

	private int StringToInteger(string input)
	{
		int num;
		//string型からint型へ変換
		Int32.TryParse(input,out num);
		//入力を0~59に制限
		if(num>59)
			num = 59;
		else if(num<0)
			num = 0;
		return num;
	}
	
	public void InputMinute() 
	{
		string input;
        input = inputField.text;
		UISelectController.minute = StringToInteger(input);
    }

	public void InputSecond() 
	{
		string input;
        input = inputField.text;
		UISelectController.second = StringToInteger(input);
    }
}