using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    #region Constante
    protected const string ANIM_JUMP = "jump";
    protected const string ANIM_SPEED = "xSpeed";
    protected const string ANIM_YSPEED = "ySpeed";
    protected const string ANIM_GROUND = "ground";

    #endregion

    [System.Serializable]
    public enum PlayerType
    {
        CharacterOne = 0,
        CharacterTwo = 1
    }

    public struct AxisInputs
    {
        public string horizontal;
        public string vertical;
    }


    public PlayerType type = PlayerType.CharacterOne;

    public bool canJump = false;

    //Puede ser -1 o 1
    [SerializeField]protected int direction = 1;

    public float xInput = 0.0f;

    public float yInput = 0.0f;

    [HideInInspector]
    public InputState actionButton;

    [HideInInspector]
    public InputState jumpButton;

    [HideInInspector]
    public InputState changeButton;

    [SerializeField]
    public InputState pointButton;

    

    public string ActionButton;
    public Animator anim;

    public Rigidbody2D body;

    [SerializeField]
    public float horizontalSpeed = 1;

    [SerializeField]
    public float jumpForce = 10;

    protected Vector2 innerVelocity;

    public bool isGround = true;


    public AxisInputs axis;
    public int playerNumber = 0;

    public PlayerController otherPlayer;


    public CircleCollider2D footColission;

    public bool changeController = true;

    public Sound walkSound;

    public void Start()
    {
        InitComponents();
    }


    public void InitComponents()
    {



        setButtons();

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }


        if (footColission == null)
        {
            footColission = GetComponentInChildren<CircleCollider2D>();
        }

        if (otherPlayer == null)
        {
            this.otherPlayer = FindObjectOfType<PlayerController>();
        }
    }


    public virtual void Update()
    {
        ProcessInput();
        ProcessMovement();
        if (isGround)
        {
            DoJump();
        }
        CheckGround();
        if (changeButton.down && changeController)
        {
            ChangePlayers();
        }else{

            changeController = true;
        }
    }

    protected void DoJump()
    {
        if (canJump && jumpButton.down)
        {
            float force = jumpForce;

            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            this.anim.SetTrigger(ANIM_JUMP);
        }
    }

    protected void ProcessMovement()
    {
        float xmove = xInput * horizontalSpeed * Time.deltaTime;
        transform.Translate(new Vector3(xmove, 0, 0));

        if(Mathf.Abs(xInput)>0.01f)
            direction = (int)Mathf.Sign(xInput);

        this.anim.SetFloat(ANIM_SPEED, this.body.velocity.x);
        this.anim.SetFloat(ANIM_YSPEED, this.body.velocity.y);
    }



    protected void ProcessInput()
    {
        yInput = Input.GetAxis(axis.vertical);
        xInput = Input.GetAxis(axis.horizontal);
        actionButton.Process();
        jumpButton.Process();
        changeButton.Process();
    }


    protected void CheckGround()
    {

        Collider2D[] results = new Collider2D[1];
        int collide = this.footColission.OverlapCollider(new ContactFilter2D()
        {
            useDepth = true,
            useLayerMask = true,
            layerMask = LayerMask.GetMask("Scene"),

        }, results);



        if (collide > 0 && this.body.velocity.y < 0.01f)
        {
            this.isGround = true;

            this.anim.SetBool(ANIM_GROUND, isGround);
        }
        else
        {
            this.isGround = false;

            this.anim.SetBool(ANIM_GROUND, isGround);
        }
    }


    public void ChangePlayers()
    {
        this.otherPlayer.playerNumber = this.otherPlayer.playerNumber  == 1 ? 0 : 1;
        this.otherPlayer.setButtons();

        this.ChangeInputs();
    }

    public void ChangeInputs()
    {
        this.playerNumber = this.playerNumber == 1 ? 0 : 1;

        setButtons();

    }


    protected void setButtons()
    {   this.changeController = false;
        if (playerNumber == 0)
        {
            actionButton = new InputState("action");
            jumpButton = new InputState("Jump");
            changeButton = new InputState("change");
            pointButton = new InputState("pointer");
            axis = new AxisInputs()
            {
                vertical = "Vertical",
                horizontal = "Horizontal"
            };
        }
        else
        {
            actionButton = new InputState("action_1");
            jumpButton = new InputState("Jump_1");
            changeButton = new InputState("change_1");
            pointButton = new InputState("pointer_1");
            axis = new AxisInputs()
            {
                vertical = "Vertical_1",
                horizontal = "Horizontal_1"
            };
        }
    }


    public void Step(){
        this.walkSound.Play(transform,body);
    }
}

