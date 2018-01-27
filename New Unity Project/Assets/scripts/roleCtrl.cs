using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roleCtrl : MonoBehaviour {
    public int type = 1;
    public int main = 0;
    public TextMesh textmesh;
    public SpriteRenderer sp;
    public List<Sprite> splist;
    // Use this for initialization
    void Start () {
        switch (type) {
            case 1:
                textmesh.text = "上班族";
                sp.sprite = splist[0];
                break;
            case 2:
                textmesh.text = "家里蹲";
                sp.sprite = splist[1];
                break;
            case 3:
                textmesh.text = "老顽固";
                sp.sprite = splist[2];
                break;
            case 4:
                textmesh.text = "疯子";
                sp.sprite = splist[3];
                break;
            case 5:
                textmesh.text = "大V";
                sp.sprite = splist[4];
                break;
        }
        if(main == 1)
        {
            keyboardCtrl ctrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
            ctrl.enabled = true;
            ctrl.gameObject.tag = "mainrole";
        }

	}
	
	// Update is called once per frame
	void Update () {
        Transform camTrans = GameObject.FindWithTag("MainCamera").transform;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.LookAt(camTrans.position + camTrans.forward * 1000);
    }
}
