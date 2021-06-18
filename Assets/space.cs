using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class space : MonoBehaviour
{
    GameObject slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Slider");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))//down是一个动作
        {
            slider.SetActive(true);
            slider.GetComponent<Slider>().value+=0.1f;
        }    
        if(Input.GetKeyUp(KeyCode.Space))
        {
            slider.SetActive(false);
        }
    }
}
