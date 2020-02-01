using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallController : PlayerController
{
    // Start is called before the first frame update


    public Transform shootPoint;

    public GameObject pointer;

    public SiliconeBullet bulletPrefab;

    private int actualPool = 0;

    public GameObject poolContainer;


    private SiliconeBullet[] bulletPool;

    private Vector2 shootDir;



    public Sound shootSound;

    public bool isGrabbed;

    public int maxBullets = 5;

    public float timeBetweenShoots = 1.0f;
    private float shootDelay = 0;
    new void Start()
    {

        base.Start();
        if (shootPoint == null)
        {
            this.shootPoint = transform;
        }

        bulletPool = new SiliconeBullet[maxBullets];
        if (poolContainer)
        {
            for (int i = 0; i < this.maxBullets; i++)
            {
                this.bulletPool[i] = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, this.poolContainer.transform);
                this.bulletPool[i].gameObject.SetActive(false);
            }
        }

        isGrabbed = false;
    }

    // Update is called once per frame
    new void Update()
    {
        if(!GameManager.Instance.pause){
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
        }
        else{
            changeController = true;
        }
        ProcessShot();
        ProcessPause();
        }
    }


    protected new void ProcessMovement()
    {

        if (Mathf.Abs(xInput) > 0.01f){
            direction = (int)Mathf.Sign(xInput);
            this.transform.localScale = new Vector2(direction*Mathf.Abs(this.transform.localScale.x),this.transform.localScale.y);
        }

        if (!this.pointButton.press)
        {
            this.anim.SetBool("hold", false);
            float xmove = xInput * horizontalSpeed * Time.deltaTime;
            transform.Translate(new Vector3(xmove, 0, 0));

            this.anim.SetFloat(ANIM_SPEED, this.body.velocity.x);
            this.anim.SetFloat(ANIM_YSPEED, this.body.velocity.y);

            if (xInput != 0)
            {
                this.shootDir = new Vector2(xInput, 0).normalized  ;
            }
            pointer.SetActive(false);

            pointer.transform.position = transform.position + new Vector3(this.shootDir.x,0);
        }
        else
        {
            
            this.anim.SetBool("hold", true);

            pointer.SetActive(true);
            this.shootDir = new Vector2(xInput, yInput).normalized;

            pointer.transform.position = transform.position + new Vector3(this.shootDir.x, this.shootDir.y);
        }



    }





    protected void ProcessShot()
    {

        if (this.shootDelay > 0)
        {
            this.shootDelay -= Time.deltaTime;
        }
        else
        {
            if (actionButton.down && !isGrabbed)
            {
                var bulletInstance = this.bulletPool[actualPool];

                if (bulletInstance.isActiveAndEnabled)
                {
                    bulletInstance.Reset();
                }


                bulletInstance.Init(pointer.transform.position, Quaternion.identity, this.shootDir);


                actualPool = (actualPool + 1) % this.bulletPool.Length;
                //Instantiate(bullet, shootPoint.position, Quaternion.identity);
                shootDelay = timeBetweenShoots;

                this.shootSound.Play(transform,body);
            }
        }
    }
}
