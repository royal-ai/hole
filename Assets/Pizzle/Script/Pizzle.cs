using System.Collections;
using UnityEngine;

namespace PizzleMap
{
    public class Pizzle : MonoBehaviour
    {
        public static int[] pizzleArr = { 0, 0, 0 , 0, 0, 0, 0, 0};

        private static bool succeed = false;
        private Vector3 targetPos;
        private GameObject[] obj;

        private void Start()
        {
            obj = GameObject.FindGameObjectsWithTag("PizzleDoor");
            targetPos = obj[0].transform.position + obj[0].transform.up * 3f;
        }

        void Update()
        {
            if (succeed == true)
            {
                obj[0].transform.position = Vector3.Lerp(obj[0].transform.position, targetPos, 0.01f);
            }
            if(obj[0].transform.position==targetPos)
                succeed = false;
        }

        // ����Ƿ�ƴ��
        public static void Check()
        {
            foreach (int a in pizzleArr)
            {
                if (a != 1)
                {
                    succeed = false;
                    return;
                }
            }
            succeed = true;
            Debug.Log("succeed");
        }

        public static bool SucceedOrNot
        {
            get
            {
                return succeed;
            }
        }

        public void Change()
        {
            if (succeed == true)
            {
                targetPos = obj[0].transform.position + obj[0].transform.up * 5f;
                obj[0].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}

