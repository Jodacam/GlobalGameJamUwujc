using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SiliconeBullet : MonoBehaviour
{

    public SpriteRenderer render;

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

    private Vector2 actualDir = Vector2.zero;
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

                Vector2 deltaMove = Vector2.Scale(speed,actualDir) * Time.deltaTime;
                body.MovePosition(deltaMove);
                break;

            default:
                break;
        }
    }

    public void Reset()
    {
        this.state = BulletState.Moving;
    }

    internal void Init(Vector3 position, Quaternion rotation, Vector2 shootDir)
    {
        transform.SetPositionAndRotation(position, rotation);
        this.actualDir = shootDir;
    }
}
