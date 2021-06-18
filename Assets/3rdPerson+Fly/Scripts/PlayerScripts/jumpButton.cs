using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpButton : MonoBehaviour
{
    public rockGenerate rocks;
    public Vector3 personGamePos = new Vector3();
    private GameObject startGame;
    private GameObject failGame;
    private GameObject person;
    private MoveBehaviour moveBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        startGame = transform.GetChild(3).gameObject; //第三个是开始游戏，暂时写成这样
        person = GameObject.Find("shadow");
        failGame = GameObject.Find("JumpFail");
        failGame.SetActive(false);
        moveBehaviour = person.GetComponent<MoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveBehaviour.GameMode && person.transform.position.y < -3.0f){
            person.transform.position = personGamePos;
            failGame.SetActive(true);
        }   
    }

    public void Click()
    {
        startGame.SetActive(false);
        failGame.SetActive(false);
        person.transform.position = personGamePos;
        moveBehaviour.GameMode = true;
        rocks.update_cube();
    }

    public void exitClick(){
        failGame.SetActive(false);
        moveBehaviour.GameMode = false;
    }
}
