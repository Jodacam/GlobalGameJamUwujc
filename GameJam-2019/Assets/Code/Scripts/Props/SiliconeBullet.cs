using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SiliconeBullet : MonoBehaviour
{

    public SpriteRenderer render;

    public Sprite bulletSprite;
    public Sprite stickSprite;

    public Collider2D shootCollider;

    public Collider2D stickCollider;

    public Rigidbody2D body;

    public Vector2 speed;

    public enum BulletState
    {
        Moving,
        Stick
    }

    [HideInInspector]
    public BulletState state;

    public Vector2 actualDir = Vector2.zero;
    void Start()
    {
        if (render == null)
        {
            render = GetComponent<SpriteRenderer>();
        }
    }



    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BulletState.Moving:

                Vector2 deltaMove = Vector2.Scale(speed, actualDir) * Time.deltaTime;
                body.transform.Translate(deltaMove);
                break;

            default:
                break;
        }
    }

    public void Reset()
    {
        this.ChangeState(BulletState.Moving);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other);
    }

    private void OnCollisionEnter(Collision other) {
        CheckCollision(other.gameObject.GetComponent<Collider2D>());
    }

    private void CheckCollision(Collider2D other)
    {
        if (other.tag != "Player")
        {
            var objectFixable = other.GetComponent<IFixable>();

            if (objectFixable != null && objectFixable.CanFix())
            {
                objectFixable.Fix();
            }
            else
            {

                ChangeState(BulletState.Stick);
            }
        }
    }

    public void ChangeState(BulletState newState)
    {

        this.state = newState;

        switch (state)
        {
            case BulletState.Moving:
                this.render.sprite = this.bulletSprite;
                this.shootCollider.enabled = true;
                this.stickCollider.enabled = false;
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            
            case BulletState.Stick:
                this.gameObject.layer = LayerMask.NameToLayer("Scene");
                this.render.sprite = this.stickSprite;
                this.shootCollider.enabled = false;
                this.stickCollider.enabled = true;
                break;
            default:
                break;
        }

    }

    internal void Init(Vector3 position, Quaternion rotation, Vector2 shootDir)
    {
        transform.SetParent(null);
        transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        this.actualDir = shootDir;
    }
}
