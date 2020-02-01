using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallController : PlayerController
{
    // Start is called before the first frame update


    public Transform shootPoint;
    public GameObject bullet;

    public float timeBetweenShoots = 1.0f;
    private float shootDelay = 0;
    new void Start()
    {
        
        base.Start();
        if (shootPoint == null)
        {
            this.shootPoint = transform;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        ProcessShot();
    }


    protected void ProcessShot()
    {

        if (this.shootDelay > 0)
        {
            this.shootDelay -= Time.deltaTime;
        }
        else
        {
            if (actionButton.press)
            {
                var bulletInstance = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                shootDelay = timeBetweenShoots;
            }
        }
    }
}
