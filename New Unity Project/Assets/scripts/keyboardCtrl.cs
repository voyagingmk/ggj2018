using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardCtrl : MonoBehaviour {
    float speed = 5.0f;   //移动速度
    float yOffset = 0.0f;
    float ySpd = 0.0f;
    public GameObject circlePrefab = null;
    bool hasPress = false;
    void Start () {
        yOffset = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float translationZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float yOffsetOld = yOffset;
        yOffset += ySpd;
        const float h = 1.0f;
        const float spd = 0.175f;
        if (translationX != 0.0f || translationZ != 0.0f)
        {
            // 还在移动
            if (ySpd == 0.0f)
            {
                ySpd = spd;
            }
            else
            { 
                if (yOffsetOld >= 0.0f && yOffset < 0.0f)
                {
                    ySpd = spd;
                }
                if (yOffsetOld <= h && yOffset > h)
                {
                    ySpd = -spd;
                }
            }
        }
        else
        {
            if (yOffsetOld >= 0.0f && yOffset < 0.0f)
            {
                yOffset = 0.0f;
                ySpd = 0.0f;
            }
            if (yOffsetOld <= h && yOffset > h)
            {
                ySpd = -spd;
            }
        }
        transform.parent.transform.Translate(translationX, 0, translationZ);
         transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);


        if (!hasPress && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("您按下了Space键");
            roleCtrl ctrl = gameObject.GetComponentInParent<roleCtrl>();
            emitCircle(ctrl.circleRadius);
            hasPress = true;
            ctrl.check = true;
        }
    }
    public void emitCircle(int r)
    {
        GameObject obj = Instantiate(circlePrefab);
        circleCtrl ctrl = obj.GetComponent<circleCtrl>();
        ctrl.end = r;
        ctrl.roleID = transform.parent.gameObject.GetInstanceID();
        obj.transform.position = transform.position + new Vector3(0, 1.0f, 0);
    }
}
