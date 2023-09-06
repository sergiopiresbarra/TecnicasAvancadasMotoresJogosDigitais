using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    GameObject objGun;
    public int weaponType = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8){ 
            objGun = GameObject.Find("Weapon");
            objGun.GetComponent<Gun>().SetWeaponType(weaponType); //envia o tipo de arma que foi colidida
       }
    }
}
