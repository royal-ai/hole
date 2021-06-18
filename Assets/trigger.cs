using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trigger : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject fire_text;
    const string on_fire = "按下【E键】";
    const string off_fire = "按下【E键】";

    public bool isTrigger=false;
    Text t ;
    void Start()
    {
        fire_text=GameObject.Find("fireText");
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnTriggerEnter(Collider other) {
        isTrigger=true;
        //----------UI-----------显示
        Color c = fire_text.GetComponent<Text>().color;
        c.a=1.0f;
        fire_text.GetComponent<Text>().color=c;
        print(fire_text.GetComponent<Text>().color);
        fire_text.GetComponent<Text>().text=on_fire;
    }
    
    void OnTriggerExit(Collider other)
    {
        isTrigger=false;
        //----------UI-----------隐藏
        Color c = fire_text.GetComponent<Text>().color;
        c.a=0.0f;
        fire_text.GetComponent<Text>().color=c;
        print(fire_text.GetComponent<Text>().color);
        fire_text.GetComponent<Text>().text=off_fire;
    }
}
