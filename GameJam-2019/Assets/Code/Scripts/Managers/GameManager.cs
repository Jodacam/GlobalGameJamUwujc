using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manager del juego. Aquí podemos guardar los progresos que se van haciendo e ir compronaod con los objetos.
public class GameManager : MonoBehaviour
{
    private GameManager _instance;

    public GameManager Instance {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            } 
            return _instance;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
