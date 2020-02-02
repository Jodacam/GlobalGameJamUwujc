using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Camera cam;
    private bool following;
    private GameObject smallPlayer;
    private GameObject bigPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<Camera>();
        smallPlayer = FindObjectOfType<SmallController>().gameObject;
        bigPlayer = FindObjectOfType<BigController>().gameObject;
        following = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(following)
        {
            Vector3 middlePoint = new Vector3(smallPlayer.transform.position.x+bigPlayer.transform.position.x, smallPlayer.transform.position.y + bigPlayer.transform.position.y, 0);
            float distance = (middlePoint - transform.position).magnitude;
            Vector2 position = Vector2.Lerp(transform.position,middlePoint,Mathf.Min(1-(Mathf.Sqrt(distance)/distance),1));
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
    }
}
