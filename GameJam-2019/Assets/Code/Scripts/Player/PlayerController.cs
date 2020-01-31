using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)),RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public enum PlayerType {
        CharacterOne = 0,
        CharacterTwo = 1
    }


    public PlayerType type = PlayerType.CharacterOne;

    [HideInInspector]
    public float xInput = 0.0f;
    [HideInInspector]
    public float yInput = 0.0f;

    [HideInInspector]
    public InputState actionButton;

    public string ActionButton; 
    public Animator anim;

    public Rigidbody body;

    [SerializeField]
    public Vector2 speed = new Vector2(0,0);

    
    void Start()
    {
        InitComponents();
    }


    public void InitComponents(){


        actionButton = new InputState(ActionButton);
        if(anim == null){
            anim = GetComponent<Animator>();
        }

        if(body == null){
            body = GetComponent<Rigidbody>();
        }
    }

    
    void Update()
    {
        ProcessInput();
        ProcessMovement();


    }


    private void ProcessMovement(){
        
    }



    private void ProcessInput(){
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");
        actionButton.Process();
    }
}
