using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUsage : MonoBehaviour {

    [Header ("Fuel Objects")]
    public Slider fuel;
    public OilHolder OilHolder;
    public GameObject TakeOilPanel;
    public Slider OilBarrelStats;
    public GameObject refillText;

    [Header ("Lamp Objects")]
    public GameObject LampInfo;
    public Text LampStatus;

    [Header ("Eat and Drink Objects")]
    public LifeStats stats;
    public GameObject EatPanel;
    public Slider EatSlider;
    public GameObject DrinkPanel;


    [Header ("Punch Objects")]
    public HandInventory inventory;
    public Animator punchAnim;
    public GameObject pickaxeSparks;

    [Header ("Grabbing Objects")]
    public GameObject GrabIcon;
    public SpringJoint joint;
    bool grabbing = false;

    [Header("Generator Objects")]
    public GameObject GeneratorPanel;
    public GameObject RefillPanelGenerator;
    public Slider GenFuelSlider;
    public Text GeneratorStatus;

    [Header("Spotlight Objects")]
    public GameObject SpotlightPanel;
    public Slider SpotDistanceSlider;
    public Text SpotlightStatus;


    [Header ("Other objects")]
    public Camera PlayerCam;













	void Update ()
    {
        RaycastHit hit;
        Ray ray = PlayerCam.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            if (inventory.selected == 0)
            {
                punchAnim.SetTrigger("PunchTorch");
                if (Physics.Raycast(ray, out hit, 4f))
                {
                    if(hit.transform.tag == "AI")
                    {
                        AIController AI = hit.transform.GetComponent<AIController>();
                        AI.Health -= 20f;
                        AI.Agressive = true;
                        AI.PathFinder = false;
                        AI.DetectionRange += 5f;
                    }
                }
            }
            if (inventory.selected == 2)
            {

                punchAnim.SetTrigger("PunchPick");


                if (Physics.Raycast(ray, out hit, 4f))
                {

                    GameObject PickSpark = Instantiate(pickaxeSparks, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(PickSpark, 3f);
                    if(hit.transform.tag == "Mineable")
                    {
                        Mineable mine = hit.transform.GetComponent<Mineable>();

                        mine.Health -= 50f;
                        mine.MineRefresh();
                        mine.Save();

                    }



                    if (hit.transform.tag == "AI")
                    {
                        AIController AI = hit.transform.GetComponent<AIController>();
                        AI.Health -= 50f;
                        AI.Agressive = true;
                        AI.PathFinder = false;
                        AI.DetectionRange += 5f;
                    }
                }
            }
        }
        if(Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.transform.tag == "SpotLight")
            {
                Spotlight spotlight = hit.transform.GetComponent<Spotlight>();
                SpotlightPanel.SetActive(true);
                SpotDistanceSlider.value = spotlight.distance;
                SpotDistanceSlider.maxValue = spotlight.MaxDistance;
                if(spotlight.Toggled == true)
                {
                    SpotlightStatus.text = "Enabled";
                    SpotlightStatus.color = Color.green;
                }
                else
                {
                    if(spotlight.distance > spotlight.MaxDistance)
                    {
                        SpotlightStatus.text = "Out of range";
                        SpotlightStatus.color = Color.red;
                    }
                    else
                    {
                        if(spotlight.generator.GetComponent<Generator>().Toggled == true)
                        {
                            SpotlightStatus.text = "Disabled";
                            SpotlightStatus.color = Color.yellow;
                        }
                        else
                        {
                            SpotlightStatus.text = "Unpowered";
                            SpotlightStatus.color = Color.cyan;
                        }

                    }
                }

                if(Input.GetKeyDown("e"))
                {
                    spotlight.Toggle();
                    spotlight.Save();
                }
            }
        }


        if(Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.transform.tag == "Generator")
            {
                GenFuelSlider.value = hit.transform.GetComponent<Generator>().CurrentFuel;
                if(hit.transform.GetComponent<Generator>().Toggled == true)
                {
                    GeneratorStatus.text = "Running";
                    GeneratorStatus.color = Color.green;
                }
                else
                {
                    if(hit.transform.GetComponent<Generator>().CurrentFuel >= 3)
                    {
                        GeneratorStatus.text = "Stopped";
                        GeneratorStatus.color = Color.yellow;
                    }
                    else
                    {
                        GeneratorStatus.text = "Out of fuel";
                        GeneratorStatus.color = Color.red;
                    }
                }
                GeneratorPanel.SetActive(true);
                if(Input.GetKeyDown("e"))
                {
                    hit.transform.GetComponent<Generator>().Toggle();
                    hit.transform.GetComponent<Generator>().SaveStats();
                }
                if(inventory.selected == 1 && OilHolder.HoldingNow >= 1)
                {
                    RefillPanelGenerator.SetActive(true);
                    if(Input.GetKeyDown("r"))
                    {
                        OilHolder.HoldingNow--;
                        hit.transform.GetComponent<Generator>().CurrentFuel += 1000;
                        hit.transform.GetComponent<Generator>().SaveStats();
                        OilHolder.Save();
                    }

                }
                else
                {
                    RefillPanelGenerator.SetActive(false);
                }
            }
        }


        if(Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.transform.tag == "Eatable" && hit.transform.GetComponent<Eatable>().FoodAmount > 1)
            {
                EatPanel.SetActive(true);
                EatSlider.maxValue = hit.transform.GetComponent<Eatable>().MaxFood;
                EatSlider.value = hit.transform.GetComponent<Eatable>().FoodAmount;
                if (Input.GetKeyDown("e"))
                {
                    if(hit.transform.GetComponent<Eatable>().FoodAmount > 1)
                    {
                        if(stats.Hunger < 950 || stats.Thirst < 950)
                        {
                            stats.Hunger += hit.transform.GetComponent<Eatable>().SaturationAmount;
                            stats.Thirst += hit.transform.GetComponent<Eatable>().WaterAmount;
                            stats.Health += 100;
                            hit.transform.GetComponent<Eatable>().FoodAmount--;
                            hit.transform.GetComponent<Eatable>().Save();
                        }

                        
                    }
                }
            }
        }


        if(Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.transform.tag == "OilBarrel")
            {
                OilBarrelStats.maxValue = hit.transform.GetComponent<OilBarrel>().MaxFuel;
                OilBarrelStats.value = hit.transform.GetComponent<OilBarrel>().fuel;
                TakeOilPanel.SetActive(true);
                if(Input.GetKeyDown("e"))
                {
                    if(hit.transform.GetComponent<OilBarrel>().fuel >= 2 && OilHolder.HoldingNow == 0)
                    {
                            OilHolder.HoldingNow += 2;
                            hit.transform.GetComponent<OilBarrel>().fuel -= 2;
                            OilHolder.Save();
                            hit.transform.GetComponent<OilBarrel>().Save();

                    }
                    else
                    {
                        if (hit.transform.GetComponent<OilBarrel>().fuel >= 1)
                        {
                            if (OilHolder.HoldingNow != 2)
                            {
                                OilHolder.HoldingNow += 1;
                                hit.transform.GetComponent<OilBarrel>().fuel -= 1;
                                OilHolder.Save();
                                hit.transform.GetComponent<OilBarrel>().Save();
                            }

                        }
                    }

                }
            }
        }

        if(Physics.Raycast(ray, out hit, 2f))
        {
            if(hit.transform.tag == "DrinkAble")
            {
                if(hit.transform.GetComponent<WaterDrinkable>().WaterAmount >= 1)
                {
                    DrinkPanel.SetActive(true);
                    if (Input.GetKeyDown("e"))
                    {
                        if(stats.Thirst <= 950)
                        {
                            stats.Thirst += 170;
                            hit.transform.GetComponent<WaterDrinkable>().WaterAmount -= 1;
                            hit.transform.GetComponent<WaterDrinkable>().Save();
                        }

                    }
                }
            }

        }


        if(Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                GrabIcon.SetActive(true);

                if (Input.GetMouseButton(1))
                {
                    joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                    grabbing = true;
                }
                if(Input.GetMouseButtonUp(1))
                {
                    joint.connectedBody = null;
                    GrabIcon.SetActive(false);
                    grabbing = false;
                }

            }

        }




        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag != "SpotLight")
            {
                SpotlightPanel.SetActive(false);
            }
            if(hit.transform.tag != "Generator")
            {
                GeneratorPanel.SetActive(false);
            }
            if (hit.transform.tag != "OilBarrel")
            {
                TakeOilPanel.SetActive(false);
            }
            if (hit.transform.tag != "DrinkAble")
            {
                DrinkPanel.SetActive(false);
            }
            if(hit.transform.tag != "Eatable")
            {
                EatPanel.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                joint.connectedBody = null;
                GrabIcon.SetActive(false);
                grabbing = false;
            }

            if (hit.transform.tag != "LampControl")
            {
                LampInfo.SetActive(false);
            }
            
            if (hit.transform.GetComponent<Rigidbody>() == null)
            {
                if(grabbing == false)
                {
                    GrabIcon.SetActive(false);
                }

            }
        }


        if(Physics.Raycast(ray, out hit, 2f))
        {

            if (hit.transform.tag == "LampControl") //LAMP CONTROLLER
            {
                Debug.Log("Ray hit lamp " + hit.transform.name);
                Transform objHit = hit.transform;
                fuel.value = objHit.GetComponent<Lantern>().Fuel;
                LampInfo.SetActive(true);

                if(OilHolder.HoldingNow > 0 && inventory.selected == 1)
                {
                    if(objHit.GetComponent<Lantern>().Fuel < 950)
                    {
                        refillText.SetActive(true);
                        if (Input.GetKeyDown("r"))
                        {
                            OilHolder.HoldingNow--;
                            objHit.GetComponent<Lantern>().Fuel += 900f;
                            OilHolder.Save();
                            objHit.GetComponent<Lantern>().SaveSettings();
                        }
                    }

                }
                else
                { refillText.SetActive(false); }
                
                if(objHit.GetComponent<Lantern>().toggled == true)
                {
                    LampStatus.text = "Active";
                    LampStatus.color = Color.green;
                }
                if (objHit.GetComponent<Lantern>().toggled == false)
                {
                    if(objHit.GetComponent<Lantern>().Fuel >= 1.5f)
                    {
                        LampStatus.text = "Off";
                        LampStatus.color = Color.yellow;
                    }
                    else
                    {
                        LampStatus.text = "Out of fuel";
                        LampStatus.color = Color.red;
                    }

                }

                if (Input.GetKeyDown("e"))
                {

                      if(objHit.GetComponent<Lantern>().Fuel >= 1f )
                    {
                        objHit.GetComponent<Lantern>().Toggle();
                        objHit.GetComponent<Lantern>().SaveSettings();
                    }

                }


            }



        }

	}
}
