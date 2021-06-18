using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzleMap;

public class Cube : MonoBehaviour
{
    public Rigidbody rigid; // �����ײ
    private RaycastHit hit; // ����
    public int accu; // ׼ȷλ��
    public int no; // ��ǰλ��
    private Vector3 targetPos; // �ƶ�Ŀ�ĵ�����
    public bool click = false; // �����ж��Ƿ��ƶ���λ
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private int oldNo;
    private bool recovery = false;
    public Vector3 oldPos;

    void Start()
    {
        targetPos = transform.position; // û���ʱ�����ƶ�
        rigid = GetComponent<Rigidbody>(); // �����ײ
        oldNo = no;
        oldPos = transform.position;
        StartCoroutine(OnMouseDown());
    }

    void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        if (click == true)
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        if ((transform.position - targetPos).sqrMagnitude<0.000001f)
            click = false;
        if(RecoveryCheck()==true)
        {
            recovery = false;
        }
    }

    IEnumerator OnMouseDown()
    {
        while (Input.GetMouseButtonDown(0) && click == false)
        {
            click = true;
            //Debug.Log("down");
            Vector3 move = Judge();
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + move*1.01f;
            //Debug.Log(move);
            Check();
            Pizzle.Check();
            yield return new WaitForFixedUpdate(); //�������Ҫ��ѭ��ִ��
        }
    }

    // �ж��Ǹ���������ƶ���ͬʱ�ı��� no ֵ
    Vector3 Judge()
    {
        Vector3 vec3 = new Vector3(0f, 0f, 0f);
        if (!rigid.SweepTest(transform.up, out hit, 1.0f))
        {
            vec3 = transform.up;
            no -= 3;
        }
        else if (!rigid.SweepTest(-transform.up, out hit, 1.0f))
        {
            vec3 = -transform.up;
            no += 3;
        }
        else if (!rigid.SweepTest(transform.right, out hit, 1.0f))
        {
            vec3 = transform.right;
            no -= 1;
        }
        else if (!rigid.SweepTest(-transform.right, out hit, 1.0f))
        {
            vec3 = -transform.right;
            no += 1;
        }
        return vec3;
    }

    // ����Ƿ�ƴ��
    bool Check()
    {
        if (no == accu)
        {
            Pizzle.pizzleArr[accu] = 1;
            return true;
        }
        else
        {
            Pizzle.pizzleArr[accu] = 0;
            return false;
        }
    }

    bool RecoveryCheck()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            recovery = true;
            no = oldNo;
            click = false;
            transform.position = oldPos;
            targetPos = oldPos;
            return recovery;
        }
        return recovery;
    }
}
