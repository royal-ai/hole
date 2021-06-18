using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class IK2 : MonoBehaviour
{
    public GameObject lefthand;
    private Animator animator;
    public Transform target;
    public Transform hint;
    public Vector3 tempPos; //变化的位置
    public Vector3 prevPos;
    // Start is called before the first frame update
    public bool getTheTorch = false;
    public bool fire = false;
    private AvatarIKGoal g  = AvatarIKGoal.LeftHand;
    private AvatarIKHint h =  AvatarIKHint.LeftElbow;
    private GameObject torch;
    void Start()
    {
        lefthand = GameObject.Find("LeftHandMiddle1");
        GameObject gameObject = GameObject.Find("Torch");
        gameObject.SetActive(false);
        torch = GameObject.Instantiate(gameObject);
        torch.transform.localScale = new Vector3(4.0f,4.0f,4.0f);
        animator = GetComponent<Animator>();
        tempPos = lefthand.transform.position;
    }
    // private void OnAnimatorIK(int layerIndex){
    //     tempPos = Vector3.Lerp(tempPos,target.position,0.003f);
    //     if(!fire){
    //         prevPos = 
    //     }
        
    // }
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R)){
            if(getTheTorch){
                getTheTorch = false;
                animator.SetBool("GetTouch",false);
                torch.SetActive(false);
            }
            else {
                torch.SetActive(true);
                getTheTorch = true;
                animator.SetBool("GetTouch",true);
                
                torch.transform.parent = lefthand.transform;
                torch.transform.position = lefthand.transform.position;
                torch.transform.localEulerAngles = new Vector3(-80.293f,-75.822f,-12.508f);
            }
        }
    }
}
