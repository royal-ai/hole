using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    public GameObject PlantObject;

    public void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("Planter"))
        {
            GameObject PicklePlant = Instantiate(PlantObject, transform.position, new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }
    }
}
