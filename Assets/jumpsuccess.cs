using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class jumpsuccess : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject t;
    Color originColor;

    public bool isDisappear;
    void Start()
    {
        t=GameObject.Find("Canvas/JumpSuccess");
        originColor=t.GetComponent<Text>().color;
        originColor.a=0.0f;
        t.GetComponent<Text>().color=originColor;
        isDisappear=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(originColor.a-0.0f > 0 && isDisappear)
        {
            originColor.a-=0.001f;
            t.GetComponent<Text>().color=originColor;
        }
    }
    private void disappear()
    {
        isDisappear=true;
    }
    public void success()
    {
        isDisappear=true;
        originColor.a=1.0f;
        t.GetComponent<Text>().color=originColor;
        Invoke("disappear",3);
    }
}

