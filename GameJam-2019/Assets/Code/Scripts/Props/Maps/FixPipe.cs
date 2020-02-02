using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPipe : MapObject, IFixable
{

    public Sprite[] sprites;

    public SpriteRenderer render;

    public string fixName;

    public bool isFixed = false;

    public bool CanFix()
    {
       return isFixed;
    }

    public void Fix()
    {
        GameManager.Instance.SetFix(fixName,true);
    }

    public override void OnMapLoad()
    {
       isFixed =  GameManager.Instance.getStateFixed(fixName);
       if(isFixed){
           render.sprite = sprites[1];
       }
    }

    // Start is called before the first frame update
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        GameManager.Instance.RegisterKey(fixName);
    }

    
}
