using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour
{
    public bool isTrigger = false;
    GameObject beginJump;
    GameObject jumpText;
    GameObject canvas;
    jumpButton button;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        button = canvas.GetComponent<jumpButton>();
        beginJump=canvas.transform.Find("beginJump").gameObject;
        jumpText=canvas.transform.Find("JumpText").gameObject;
        beginJump.SetActive(false);
        jumpText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(isTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                beginJump.SetActive(true);
                jumpText.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        button.personGamePos = this.transform.position + this.transform.forward * 4.0f + this.transform.right * 3.5f;
        //----------UI-----------显示
        jumpText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        //----------UI-----------隐藏
        jumpText.SetActive(false);
    }
}
