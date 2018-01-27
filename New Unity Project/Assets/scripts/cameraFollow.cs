using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 20, -17);
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
        transform.position = followObject.transform.parent.transform.position + offset;

    }
}
