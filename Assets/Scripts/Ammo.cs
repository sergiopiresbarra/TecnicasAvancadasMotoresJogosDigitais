using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float m = 0f;

    public int qtdBBs = 0;
    GameObject objGun;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8){ 
            objGun = GameObject.Find("Weapon");
            objGun.GetComponent<Gun>().SetMass(m);
            objGun.GetComponent<Gun>().SetQtdBBs(qtdBBs);
       }
    }
}
