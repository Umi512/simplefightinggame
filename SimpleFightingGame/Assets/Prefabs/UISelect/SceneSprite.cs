using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//dropdownでscene変更時に画像変更
public class SceneSprite : MonoBehaviour
{ 
    private SpriteRenderer MainSpriteRenderer;
    public Sprite StageSprite1;
    public Sprite StageSprite2;

    private void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(UISelectController.FightingScene == "SimpleStage")
            MainSpriteRenderer.sprite = StageSprite1;
        else if(UISelectController.FightingScene == "StairStage")    
            MainSpriteRenderer.sprite = StageSprite2;
    }
}