using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IFixable
{

    public bool activeToggle = false;

    public Sprite active;
    public Sprite notActive;

    [Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    public ToggleEvent toggle;

    public SpriteRenderer render;
    public Sound leverSound;

    public bool CanFix()
    {
        return true;
    }

    public void Fix()
    {
        this.activeToggle = !this.activeToggle;
        leverSound.Play(transform);
        ChangeSprite();
        this.toggle.Invoke(activeToggle);
    }

    void Start()
    {
        ChangeSprite();
    }


    void ChangeSprite()
    {
        if (activeToggle)
        {
            this.render.sprite = this.active;
            leverSound.SetParameter("On-Off",0);
        }
        else
        {
            this.render.sprite = this.notActive;
            leverSound.SetParameter("On-Off",1);

        }
    }
}
