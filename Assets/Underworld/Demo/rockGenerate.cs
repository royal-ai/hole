using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockGenerate : MonoBehaviour
{
    public float min_x = -10 , max_x = 10;
    public float min_z = -2.5f , max_z = 2.5f;
    public int numOfcubes = 16;

    public float gap= 0.4F; // 假设两个方块的x轴的距离至少为2*gap 
    private float interval_x ;
    private const string cubename = "cube";
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        interval_x = (max_x-min_x)/numOfcubes;
        print(interval_x);
        //update_cube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void update_cube()
    {
        
        for(int i=0; i<numOfcubes; ++i)
        {
            GameObject cube = transform.GetChild(i).gameObject;
            double x1 = min_x+i*interval_x+gap;
            double sample = rand.NextDouble();
            float range = interval_x-2*gap;
            double cur_x = (sample*range)+x1;
            double sample2 = rand.NextDouble();
            double cur_z = (sample2*(max_z-min_z))+min_z;
            print(cur_x);
            print(cur_z);
            cube.GetComponent<Transform>().localPosition = new Vector3((float)cur_x,0,(float)cur_z);
            cube.GetComponent<Transform>().localScale = new Vector3(1.5f,1.5f,1.5f);

            // oldPosition.x = (float)cur_x;
            // oldPosition.z = (float)cur_z;
            // cube.GetComponent<Transform>().localPosition = oldPosition;
            print(cube.GetComponent<Transform>().localPosition);
            //cube.GetComponent<MeshRenderer>().enabled=false;
        }   
    }
}
