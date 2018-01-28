using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardCtrl : MonoBehaviour {
    float yOffset = 0.0f;
    public float ySpd = 0.0f;
    public GameObject circlePrefab = null;
    bool hasPress = false;
    void Start () {
        yOffset = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        roleCtrl ctrl = gameObject.GetComponentInParent<roleCtrl>();
        float translationZ = 0;
        float translationX = 0;
        if (ctrl.main)
        {
            translationZ = Input.GetAxis("Vertical") * defines.MoveSpeed * Time.deltaTime;
            translationX = Input.GetAxis("Horizontal") * defines.MoveSpeed * Time.deltaTime;
        }
        float yOffsetOld = yOffset;
        yOffset += ySpd;
        const float h = 1.0f;
        float spd = 10.0f * Time.deltaTime;
        if (!hasPress && (translationX != 0.0f || translationZ != 0.0f))
        {
            if (ySpd == 0.0f)
            {
                ySpd = spd;
            }
            else
            {

            }
        }
        else
        {
            if (yOffsetOld >= 0.0f && yOffset < 0.0f)
            {
                yOffset = 0.0f;
                ySpd = 0.0f;
            }
        }
        if (yOffsetOld >= 0.0f && yOffset < 0.0f)
        {
            ySpd = spd;
        }
        if (yOffsetOld <= h && yOffset > h)
        {
            ySpd = -spd;
        }
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        if (!hasPress)
        {
            transform.parent.transform.Translate(translationX, 0, translationZ);
            if (ctrl.main && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("您按下了Space键");
                ctrl.beginEmitNoJump();
                hasPress = true;
                ctrl.check = true;
            }
        }

    }
    public void emitCircle(int r, bool jump)
    {
        GameObject obj = Instantiate(circlePrefab);
        circleCtrl ctrl = obj.GetComponent<circleCtrl>();
        ctrl.end = r;
        ctrl.roleID = transform.parent.gameObject.GetInstanceID();
        obj.transform.position = transform.position + new Vector3(0, 1.0f, 0);

        roleCtrl rCtrl = gameObject.GetComponentInParent<roleCtrl>();
        stageCtrl sCtrl = GameObject.FindGameObjectWithTag("stageCtrl").GetComponent<stageCtrl>();
        rCtrl.Say(sCtrl.gameType, jump);
    }
}
