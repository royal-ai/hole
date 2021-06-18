using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOff : MonoBehaviour
{
    public bool isFire;
    GameObject fire;
    GameObject torch;
    GameObject light;
    GameObject Particles;
    GameObject parent;
    GameObject Quad;
    
    float iniLightIntensity;
    // Start is called before the first frame update
    void Start()
    {        
        fire = transform.GetChild(0).gameObject;
        light = transform.GetChild(1).gameObject;
        Particles=transform.GetChild(2).gameObject;
        parent = transform.parent.gameObject;
        Quad= parent.transform.GetChild(1).gameObject;

        iniLightIntensity = light.GetComponent<Light>().intensity;
        light.GetComponent<Light>().intensity=0;
        isFire = false;
        Particles.GetComponent<ParticleSystem>().Stop();
        fire.GetComponent<ParticleSystem>().Stop();
    }
void turn()
    {
        if(isFire)
        {
            Particles.GetComponent<ParticleSystem>().Stop();
            fire.GetComponent<ParticleSystem>().Stop();
            isFire = false;
            light.GetComponent<Light>().intensity = 0;
        }
        else
        {
            isFire = true;
            Particles.GetComponent<ParticleSystem>().Play();
            fire.GetComponent<ParticleSystem>().Play();
            light.GetComponent<Light>().intensity = iniLightIntensity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Quad.GetComponent<trigger>().isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            turn(); 
        }
    }
    
}
