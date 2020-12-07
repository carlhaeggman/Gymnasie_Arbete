using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrafter : MonoBehaviour
{
    GameObject firstComponent;
    GameObject secondComponent;
    GameObject thirdComponent;

    public GameObject weapTemplate;
    

   
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void CraftWeapon()
    {
       var newWeapon = Instantiate(weapTemplate);
        firstComponent.transform.parent = newWeapon.transform;
        secondComponent.transform.parent = newWeapon.transform;
        thirdComponent.transform.parent = newWeapon.transform;
    }



   
}
