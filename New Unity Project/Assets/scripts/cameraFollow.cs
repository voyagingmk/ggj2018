using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 20, -17);
    public float t = 0.0f;
    public bool tweening = false;
    public GameObject lastOne;
    public Sequence mySequence;
    // Use this for initialization
    void Start ()
    {
        mySequence = null;
        lastOne = null;
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

    }

    public void tweenToNext(GameObject tweenToObj)
    {
        GameObject followObject = GameObject.FindWithTag("mainrole");
        Vector3 p = followObject.transform.parent.transform.position;
        tweening = true;
        if(mySequence != null)
        {
            mySequence.Kill();
        }
        mySequence = DOTween.Sequence();
        mySequence.Append(
            transform.DOMove(tweenToObj.transform.position + offset, defines.TweenTime).SetEase(Ease.InOutQuad));
        if (lastOne)
        {
            mySequence.Append(
                transform.DOMove(lastOne.transform.position + offset, defines.TweenTime).SetEase(Ease.InOutQuad));
        }
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
