using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigController : PlayerController
{

    [SerializeField] private GameObject itemInFocus;
    [SerializeField] private GameObject holderObject;

    [SerializeField] private Vector2 throwForce;

    private bool isGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        isGrabbing = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(actionButton.down)
        {
            if(isGrabbing)
            {
                Throw();
            }
            else
            {
                Grab();
            }
        }
    }

    private void Grab()
    {
        //CAMBIAR CONDICION PARA COMPROBAR QUE ES UN OBJETO QUE SE PUEDE AGARRAR
        if(true)
        {
            isGrabbing = true;
            itemInFocus.transform.SetParent(holderObject.transform);
            itemInFocus.transform.localPosition = Vector2.zero;
        }
    }

    private void Throw()
    {
        itemInFocus.transform.SetParent(null);
        itemInFocus.GetComponent<Rigidbody2D>().AddForce(throwForce, ForceMode2D.Impulse);
        isGrabbing = false;
    }
}
