using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    public float timeOfGrowth = 240;
    public float GrowthTime;
    public float GrowthLevel = 0;
    public GameObject GrownObject;
    public string PlantType;
    public string PlantNumber = "0";
    public float size1;
    public float size2;
    public float size3;
    public float size4;
    string PlantID;

    void Start()
    {
        if(PlantNumber == "0")
        {
            PlantNumber = Random.Range(0, 99999).ToString();
        }
        string CurrentPref = PlayerPrefs.GetString("PLANTS");
        PlantID = PlantNumber + PlantType;
        if (!CurrentPref.Contains(PlantID))
        {
            if(!PlantNumber.Contains (PlantType))
            {
                PlayerPrefs.SetString("PLANTS", PlayerPrefs.GetString("PLANTS") + PlantID + ",");
            }

        }

        GrowthTime = timeOfGrowth;
        GrowthLevel = PlayerPrefs.GetFloat(PlantID + "g");
        Debug.Log(GrowthLevel);
        if(GrowthLevel == 4)
        {
            Destroy(gameObject);
        }
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(PlantID + "x", transform.position.x);
        PlayerPrefs.SetFloat(PlantID + "y", transform.position.y);
        PlayerPrefs.SetFloat(PlantID + "z", transform.position.z);
        PlayerPrefs.SetFloat(PlantID + "g", GrowthLevel);

        PlayerPrefs.Save();
    }
    void Update()
    {
        GrowthTime -= Time.deltaTime;
        if(GrowthTime <= 0)
        {
            GrowthTime = timeOfGrowth;
            GrowthLevel++;
            Save();
        }
        if(GrowthLevel == 0)
        {
            gameObject.transform.localScale = new Vector3(size1, size1, size1);
            Save();
        }
        if (GrowthLevel == 1)
        {
            gameObject.transform.localScale = new Vector3(size2, size2, size2);
            Save();
        }
        if (GrowthLevel == 2)
        {
            gameObject.transform.localScale = new Vector3(size3, size3, size3);
            Save();
        }
        if (GrowthLevel == 3)
        {
            gameObject.transform.localScale = new Vector3(size4, size4, size4);
            Save();
        }
        if (GrowthLevel == 4)
        {
            Save();
            Vector3 SLoc = new  Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject Pickle = Instantiate(GrownObject, SLoc, transform.rotation);
            Destroy(gameObject);
        }
    }

}
