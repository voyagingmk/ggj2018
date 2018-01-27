using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardCtrl : MonoBehaviour {
    float speed = 3.0f;   //移动速度
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 使用上下方向键或者W、S键来控制前进后退
        float translationZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(translationX, 0, translationZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("您按下了Space键");
        }
    }
}
