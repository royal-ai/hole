using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSuccess : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject person;
    MoveBehaviour moveBehaviour;

    public jumpsuccess j;
    void Start()
    {
        person = GameObject.Find("shadow");
        moveBehaviour = person.GetComponent<MoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){
        if(moveBehaviour.GameMode)
        {
            moveBehaviour.GameMode = false;
            j.success();
        }
        
    }
}
