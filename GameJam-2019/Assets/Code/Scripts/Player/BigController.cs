using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigController : PlayerController
{

    [SerializeField] private GameObject itemInFocus;
    [SerializeField] private GameObject holderObject;

    [SerializeField] private Vector2 throwForce;

    [SerializeField] private Sound grabSound;

    [SerializeField] private Sound throwSound;

    private Coroutine throwRutine = null;

    private bool isGrabbing;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isGrabbing = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        UpdateHolderObjectPosition();
        if (actionButton.down)
        {
            if (isGrabbing)
            {
                this.anim.SetTrigger("action");
                this.throwSound.Play(transform);

                if(this.throwRutine == null)
                    this.throwRutine = StartCoroutine(delay());
                
            }
            else
            {
                Grab();
            }
        }
    }


    private IEnumerator delay(){
        yield return new WaitForSeconds(0.5f);
        Throw();
        this.throwRutine = null;
    }

    protected override void ProcessMovement()
    {
        if (!isGrabbing)
        {
            base.ProcessMovement();
        }
        else
        {
            float xmove = xInput * horizontalSpeed * Time.deltaTime;

            if (Mathf.Abs(xInput) > 0.01f)
            {
                direction = (int)Mathf.Sign(xInput);
                this.transform.localScale = new Vector2(direction * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
            }

            this.anim.SetFloat(ANIM_SPEED, 0);
            this.anim.SetFloat(ANIM_YSPEED, this.body.velocity.y);
        }
    }

    private void Grab()
    {
        if (itemInFocus != null)
        {
            isGrabbing = true;
            itemInFocus.transform.SetParent(holderObject.transform);
            itemInFocus.transform.localPosition = Vector2.zero;
            itemInFocus.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (itemInFocus.CompareTag("Player"))
                itemInFocus.GetComponent<SmallController>().isGrabbed = true;
            this.anim.SetTrigger("action");
            this.grabSound.Play(transform);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Throw()
    {
        itemInFocus.transform.SetParent(null);
        itemInFocus.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwForce.x * direction, throwForce.y), ForceMode2D.Impulse);
        itemInFocus.GetComponent<Rigidbody2D>().gravityScale = 1;
        if (itemInFocus.CompareTag("Player"))
            itemInFocus.GetComponent<SmallController>().isGrabbed = false;
        isGrabbing = false;
        itemInFocus = null;
    }

    private void UpdateHolderObjectPosition()
    {
        holderObject.transform.localPosition = new Vector2(holderObject.transform.localPosition.x, holderObject.transform.localPosition.y);
        if (isGrabbing)
            itemInFocus.transform.localPosition = Vector2.zero;
    }

    public void SetItemInFocus(GameObject focusObject)
    {
        itemInFocus = focusObject;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("posicion caja " + collision.transform.position.y + " posicion gato " + transform.position.y);
        if (collision.gameObject.CompareTag("Block") && Mathf.Abs(collision.transform.position.y - transform.position.y) <= 1)
            anim.SetBool("push", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
            anim.SetBool("push", false);
    }
}
