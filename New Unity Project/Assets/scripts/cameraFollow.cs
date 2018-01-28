using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 20, -17);
    public float t = 0.0f;
    public bool tweening = false;
    // Use this for initialization
    void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

    }

    public void tweenToNext(GameObject tweenToObj)
    {
        GameObject followObject = GameObject.FindWithTag("mainrole");
        Vector3 p = followObject.transform.parent.transform.position;
        tweening = true;
        transform.DOMove(tweenToObj.transform.position + offset, defines.TweenTime).SetEase(Ease.InOutQuad);
  //      DOTween.To(() => transform.position, x => transform.position = x, tweenToObj.transform.position, 0.5).SetRelative().SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update ()
    {
        GameObject followObject = GameObject.FindWithTag("mainrole");
        if (!followObject)
        {
            return;
        }
        if (tweening)
        {
            return;
        }
        Vector3 p = followObject.transform.parent.transform.position;
        transform.position = p + offset;
    }
}
