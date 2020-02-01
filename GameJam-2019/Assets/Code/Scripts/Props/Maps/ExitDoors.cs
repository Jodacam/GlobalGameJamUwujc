using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoors : MonoBehaviour
{

    public List<PlayerController> players;

    public string mapToLoad = "";

    private void Start() {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var controller = other.GetComponent<PlayerController>();

            players.Add(controller);

            if (players.Count > 2)
            {
                GameManager.Instance.LoadMap(mapToLoad);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var controller = other.GetComponent<PlayerController>();

            players.Remove(controller);

        }
    }


}
