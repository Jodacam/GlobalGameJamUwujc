using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private bool active;
    private bool stayActive;
    
    [System.Serializable]
    public class Action : UnityEvent { };

    public Action positiveTrigger;
    public Action negativeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BigController>())
        {
            active = true;
            positiveTrigger.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BigController>())
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BigController>())
        {
            active = false;
            negativeTrigger.Invoke();
        }
    }

    public void DebugTestingActive()
    {
        Debug.Log("HOLA");
    }

    public void DebugTestingInactive()
    {
        Debug.Log("ADIOS");
    }


}
