using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 20, -17);
    public float t = 0.0f;
    public GameObject newFollow;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject followObject = GameObject.FindWithTag("mainrole");
        if (!followObject)
        {
            return;
        }
        Vector3 p = followObject.transform.parent.transform.position;
        if (newFollow)
        {
            if (t < 1.0f)
            {
                Vector3 p2 = newFollow.transform.position;
                transform.position = p + t * (p2 - p) + offset;
                t += 0.01f;
            }            
        }
        else
        {
            transform.position = p + offset;
        }

    }
}
