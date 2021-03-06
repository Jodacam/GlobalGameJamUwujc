﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoors : MonoBehaviour
{

    public List<PlayerController> players;

    public Sprite[] sprites;
    public string mapToLoad = "";

    public Sound openSound;
    public Sound closeSound;

    public SpriteRenderer render;
    private void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var controller = other.GetComponent<PlayerController>();

            if (controller)
            {

                var find = players.Find((e) => e == controller);

                if (find == null)
                {
                    players.Add(controller);
                    openSound.Play(transform);
                    render.sprite = sprites[1];

                    if (players.Count == 2)
                    {
                        render.sprite = sprites[2];
                        GameManager.Instance.LoadMap(mapToLoad);
                        render.sprite = sprites[0];
                    }
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var controller = other.GetComponent<PlayerController>();
            closeSound.Play(transform);
            players.Remove(controller);
            render.sprite = sprites[0];

        }
    }


}
