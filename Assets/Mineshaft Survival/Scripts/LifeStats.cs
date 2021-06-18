using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class LifeStats : MonoBehaviour {



    [Header("Stats")]
    public float Health = 1000;
    public float Thirst = 1000;
    public float Hunger = 1000;

    [Header ("Sliders")]
    public Slider HealthSlider;
    public Slider ThirstSlider;
    public Slider HungerSlider;

    [Header("DeathScreen")]
    public GameObject DeathScreen;

    void Start()
    {
        StartCoroutine(save());
        StartCoroutine(lowerStats());

        if (PlayerPrefs.GetFloat("Health") != 0)
        {
            Health = PlayerPrefs.GetFloat("Health");
            Debug.Log("Health Loaded =" + Health);
        }
        else
        {
            Health = 1000f;
        }

        if (PlayerPrefs.GetFloat("Thirst") != 0)
        {
            Thirst = PlayerPrefs.GetFloat("Thirst");
            Debug.Log("Thirst Loaded =" + Thirst);
        }
        else
        {
            Thirst = 1000f;
        }

        if (PlayerPrefs.GetFloat("Hunger") != 0)
        {
            Hunger = PlayerPrefs.GetFloat("Hunger");
            Debug.Log("Hunger Loaded =" + Hunger);
        }
        else
        {
            Hunger = 1000f;
        }

    }

    public void Respawn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
        
    }


	void Update ()
    {
        if(Health < 1)
        {
            DeathScreen.SetActive(true);
            Time.timeScale = 0.000001f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<FirstPersonController>().enabled = false;
        }
        else
        {
            DeathScreen.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<FirstPersonController>().enabled = true;
        }
        if (Health >= 1000)
        {
            Health = 1000;
        }
        if (Thirst >= 1000)
        {
            Thirst = 1000;
        }
        if (Hunger >= 1000)
        {
            Hunger = 1000;
        }

        //Set all sliders to right values
        HealthSlider.value = Health; 
        ThirstSlider.value = Thirst;
        HungerSlider.value = Hunger;	
	}
    IEnumerator save()
    {
        yield return new WaitForSeconds(6); //auto save player stats every 6 seconds
        PlayerPrefs.SetFloat("Health", Health);
        PlayerPrefs.SetFloat("Thirst", Thirst);
        PlayerPrefs.SetFloat("Hunger", Hunger);
        PlayerPrefs.Save();
        Debug.Log("Player Stats saved");
        StartCoroutine(save());
    }
    IEnumerator lowerStats() //decrease stats like thirst and hunger
    {
        yield return new WaitForSeconds(2);

        if(Thirst <= 1)
        {
            Health -= 5f; //Amount of health that should be removed when thirst = 0
        }
        else
        {
            Thirst -= 0.8f;//Amount of thirst that should be removed every 2 seconds
        }

        if (Hunger <= 1)
        {
            Health -= 7f;//Amount of health that should be removed when hunger = 0
        }
        else
        {
            Hunger -= 0.6f;//Amount of hunger that should be removed every 2 seconds
        }
        StartCoroutine(lowerStats());
    }
}
