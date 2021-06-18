using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jump : MonoBehaviour
{
    public bool isJump; //是否是游戏状态
    // Start is called before the first frame update
    GameObject slider;
    Slider s1;

    Color originColor;
    Color temp;
    Image fill_image;
    GameObject fill;
    MoveBehaviour personMove;
    void Start()
    {
        personMove = GameObject.Find("shadow").GetComponent<MoveBehaviour>();
        slider = GameObject.Find("Slider");
        fill = GameObject.Find("Slider/Fill Area/Fill");
        fill_image = fill.GetComponent<Image>();
        slider.SetActive(false);
        s1 = slider.GetComponent<Slider>();
        temp = originColor = fill_image.color;
    }
    // Update is called once per frame
    void Update()
    {
        isJump = personMove.GameMode;
        if (isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                slider.SetActive(true);
                slider.GetComponent<Slider>().value = 0;
                fill_image.color = temp = originColor;
            }
            if (slider.activeSelf)
            {
                float precision = personMove.gamePower / personMove.maxPower;
                s1.value = precision;
                temp.r = precision;
                fill_image.color = temp;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                slider.SetActive(false);
            }
        }

    }
}
