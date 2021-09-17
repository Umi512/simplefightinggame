using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// http://hiyotama.hatenablog.com/entry/2016/07/10/080000
public class DropdownScript : MonoBehaviour 
{	
	private void Start () 
	{
    
	}

	private void Update ()
	{
		
	}

	public void SelectFightingStage(Dropdown dropdown) 
	{
        switch(dropdown.value)
        {
            case 0:
		    UISelectController.FightingScene = "SimpleStage";
            break;
            case 1:
            UISelectController.FightingScene = "StairStage";
            break;
            default:
            break;
        }
    }
}