using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addShadow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.GetComponent<SpriteRenderer>().GetComponent<Renderer>().receiveShadows = true;
        transform.GetComponent<SpriteRenderer>().GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
