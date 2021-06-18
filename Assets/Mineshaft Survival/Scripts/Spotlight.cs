using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour {

    [Header("Spotlight stats")]
    public float MaxDistance = 20;
    public bool Toggled = true;
    [Header("Light Objects")]
    public Renderer lightCube;
    public GameObject Lights;
    [Header("Materials")]
    public Material LampOn;
    public Material LampOff;
    [Header("ID")]
    string SpotID;

    public float distance;

    public GameObject generator;
    public void Toggle()
    {
        Toggled = !Toggled;
    }


    public void Start()
    {
        SpotID = GetInstanceID().ToString() + "SPOTLIGHT";
        Load();
    }

    public void Update()
    {
        generator = GameObject.FindGameObjectWithTag("Generator");
        distance = Vector3.Distance(gameObject.transform.position, generator.transform.position);

        if (distance <= MaxDistance && Toggled == true && generator.GetComponent<Generator>().Toggled == true)
        {
            lightCube.material = LampOn;
            Lights.SetActive(true);
        }
        else
        {
            Toggled = false;
        }
        if(Toggled == false)
        {
            lightCube.material = LampOff;
            Lights.SetActive(false);
        }
    }

    public void Save()
    {
        if(Toggled)
        {
            PlayerPrefs.SetInt(SpotID, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SpotID, 0);
        }
    }
    public void Load()
    {
        if(PlayerPrefs.GetInt(SpotID) == 1)
        {
            Toggled = true;
        }
        else
        {
            Toggled = false;
        }
    }
}
