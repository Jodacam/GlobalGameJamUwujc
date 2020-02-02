using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform startPoint;

    public Transform endPoint;

    public float speed = 3;

    public int moveState = 0;


    void Start()
    {
        if (startPoint == null)
        {
            this.startPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.moveState)
        {
            case 1:

                var dir = endPoint.position - transform.position;

                if (Vector3.Distance(endPoint.position,transform.position) <= 0.1f)
                {
                    moveState = 0;
                }
                else
                {


                    dir = dir.normalized;

                    transform.Translate(dir * Time.deltaTime * speed);
                }

                break;
            case 2:
                var dirBack = startPoint.position - transform.position;

                if (Vector3.Distance(transform.position,startPoint.position) <= 0.1)
                {
                    moveState = 0;
                }
                else
                {


                    dirBack = dirBack.normalized;

                    transform.Translate(dirBack * Time.deltaTime * speed);
                }
                break;
            default:
                break;
        }
    }

    public void MoveToEnd(bool inverse){
        this.moveState = inverse ? 1: 2;
    }

}
