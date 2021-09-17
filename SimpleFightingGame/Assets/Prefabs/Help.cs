using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//戦闘helpテキスト表示
public class Help : MonoBehaviour 
{
    private GameObject helpText;
    private bool inputH = true;
    private void Start()
    {
        helpText = GameObject.Find ("HelpShow");
    }

    private void Update()
    {
        if(Input.GetKeyDown (KeyCode.H) && inputH == true)  
        {
            helpText.GetComponent<Text> ().text = ("↑:Jump ←:Left →:Right"+"\n"+"Space:Attack or Get gun");
            inputH=false;
        }
        else if(Input.GetKeyDown (KeyCode.H) && inputH == false)
        {
            helpText.GetComponent<Text> ().text = ("");
            inputH=true;
        }
    } 

    private void Wait() 
    {
        
    }
}