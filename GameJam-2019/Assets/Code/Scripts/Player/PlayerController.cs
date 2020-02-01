using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    #region Constante
    const string ANIM_JUMP = "jump";
    const string ANIM_SPEED = "xSpeed";
    const string ANIM_YSPEED = "ySpeed";
    const string ANIM_GROUND = "ground";

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
    public float xInput = 0.0f;

    public float yInput = 0.0f;

    [HideInInspector]
    public InputState actionButton;

    [HideInInspector]
    public InputState jumpButton;

    public string ActionButton;
    public Animator anim;

    public Rigidbody2D body;

    [SerializeField]
    public float horizontalSpeed = 1;

    [SerializeField]
    public float jumpForce = 10;

    private Vector2 innerVelocity;

    public bool isGround = true;


    public AxisInputs axis;
    public int playerNumber = 0;


    public CircleCollider2D footColission;

    void Start()
    {
        InitComponents();
    }


    public void InitComponents()
    {


        if (playerNumber == 0)
        {
            actionButton = new InputState("action");
            jumpButton = new InputState("Jump");

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
            axis = new AxisInputs()
            {
                vertical = "Vertical_1",
                horizontal = "Horizontal_1"
            };
        }


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
    }


    protected virtual void Update()
    {
        ProcessInput();
        ProcessMovement();
        if (isGround)
        {
            DoJump();
        }
        CheckGround();

    }

    private void DoJump()
    {
        if (canJump && jumpButton.press)
        {
            float force = jumpForce;

            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            this.anim.SetTrigger(ANIM_JUMP);
        }
    }

    private void ProcessMovement()
    {
        float xmove = xInput * horizontalSpeed * Time.deltaTime;
        transform.Translate(new Vector3(xmove, 0, 0));

        this.anim.SetFloat(ANIM_SPEED, this.body.velocity.x);
        this.anim.SetFloat(ANIM_YSPEED, this.body.velocity.y);
    }



    private void ProcessInput()
    {
        yInput = Input.GetAxis(axis.vertical);
        xInput = Input.GetAxis(axis.horizontal);
        actionButton.Process();
        jumpButton.Process();
    }


    private void CheckGround()
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
}
