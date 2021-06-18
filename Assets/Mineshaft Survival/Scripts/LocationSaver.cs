using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSaver : MonoBehaviour {

    [Header ("Objects position")]
    public float Xpos;
    public float Ypos;
    public float Zpos;
    [Header("Objects rotation")]
    public float Xrot;
    public float Yrot;
    public float Zrot;

    //[Header ("Objects ID")]
    string ObjID; 

    Vector3 Pos;
    Quaternion Rot;


    public void LoadPos()
    {
        if (PlayerPrefs.GetFloat("Xpos" + ObjID) == 0)
        {
            Xpos = transform.position.x;
        }
        else
        {
            Xpos = PlayerPrefs.GetFloat("Xpos" + ObjID);
        }

        if (PlayerPrefs.GetFloat("Ypos" + ObjID) == 0)
        {
            Ypos = transform.position.y;
        }
        else
        {
            Ypos = PlayerPrefs.GetFloat("Ypos" + ObjID);
        }

        if (PlayerPrefs.GetFloat("Zpos" + ObjID) == 0)
        {
            Zpos = transform.position.z;
        }
        else
        {
            Zpos = PlayerPrefs.GetFloat("Zpos" + ObjID);
        }

        Pos.Set(Xpos, Ypos, Zpos);
        transform.position = Pos;

        Debug.Log("Loaded " + transform.name + "'s Location " + Xpos + " " + Ypos + " " + Zpos);
    }


    public void SavePos()
    {
        Xpos = transform.position.x;
        Ypos = transform.position.y;
        Zpos = transform.position.z;


        PlayerPrefs.SetFloat("Xpos" + ObjID, Xpos);
        PlayerPrefs.SetFloat("Ypos" + ObjID, Ypos);
        PlayerPrefs.SetFloat("Zpos" + ObjID, Zpos);

        PlayerPrefs.Save();
        Debug.Log("Saved " + transform.name +  "'s Location " + Xpos + " " + Ypos + " " + Zpos);
    }



    public void LoadRot()
    {
        if (PlayerPrefs.GetFloat("Xrot" + ObjID) == 0)
        {
            Xrot = transform.rotation.eulerAngles.x;

        }
        else
        {
            Xrot = PlayerPrefs.GetFloat("Xrot" + ObjID);
        }

        if (PlayerPrefs.GetFloat("Yrot" + ObjID) == 0)
        {
            Yrot = transform.rotation.eulerAngles.y;
        }
        else
        {
            Yrot = PlayerPrefs.GetFloat("Yrot" + ObjID);
        }

        if (PlayerPrefs.GetFloat("Zrot" + ObjID) == 0)
        {
            Zrot = transform.rotation.eulerAngles.z;
        }
        else
        {
            Zrot = PlayerPrefs.GetFloat("Zrot" + ObjID);
        }

        Rot = Quaternion.Euler(Xrot, Yrot, Zrot);
        transform.rotation = Rot;

        Debug.Log("Loaded " + transform.name + "'s Rotation " + Xrot + " " + Yrot + " " + Zrot);
    }


    public void SaveRot()
    {
        Xrot = transform.eulerAngles.x;
        Yrot = transform.eulerAngles.y;
        Zrot = transform.eulerAngles.z;


        PlayerPrefs.SetFloat("Xrot" + ObjID, Xrot);
        PlayerPrefs.SetFloat("Yrot" + ObjID, Yrot);
        PlayerPrefs.SetFloat("Zrot" + ObjID, Zrot);

        PlayerPrefs.Save();
        Debug.Log("Saved " + transform.name + "'s Rotation " + Xrot + " " + Yrot + " " + Zrot);
    }











    void Start ()
    {
        ObjID = GetInstanceID().ToString();
        LoadPos();
        LoadRot();
        StartCoroutine(AutoSave());

    }
	
    
    IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(5);
        SavePos();
        SaveRot();
        StartCoroutine(AutoSave());
    }
}
