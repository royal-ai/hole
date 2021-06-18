using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizzleTrigger : MonoBehaviour
{
    private bool isTrigger = false;
    private Vector3 camPos = new Vector3(2.94f,1.84f,0.4999995f);
    private Vector3 camRot = new Vector3(0.254f,-67.532f,-1.924f);

    GameObject pizzleText;
    GameObject personMesh;
    GameObject person;
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        person = GameObject.Find("shadow");
        personMesh = GameObject.Find("shadow_mesh");
        pizzleText=GameObject.Find("Canvas/pizzleText");
        pizzleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger){
            if(Input.GetKeyDown(KeyCode.Escape)){
                personMesh.SetActive(true);
                mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().enabled = true;
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                pizzleText.SetActive(false);
                person.transform.position = new Vector3(-50.97177f,-0.06273657f,0.9257192f);
                person.transform.rotation = Quaternion.Euler(0.0f,-76.404f,0.0f);
                mainCamera.transform.localPosition = camPos;
                mainCamera.transform.localEulerAngles = camRot;
                personMesh.SetActive(false);
                mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        //----------UI-----------显示
        pizzleText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        //----------UI-----------隐藏
        pizzleText.SetActive(false);
    }
}
