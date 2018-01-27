using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roleCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform camTrans = GameObject.FindWithTag("MainCamera").transform;
        transform.LookAt(camTrans.position + camTrans.forward * 1000);
    }
}
