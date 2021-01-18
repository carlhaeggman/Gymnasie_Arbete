using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrafter : MonoBehaviour
{
    public GameObject firstComponent, secondComponent, thirdComponent;
    public GameObject weapTemplate;
    PlayerWeapSstm weapSstmAccess;
    public GameObject player;
    public Transform weaponHolder;


    private void Start()
    {
        weapSstmAccess = player.GetComponent<PlayerWeapSstm>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CraftWeapon();
        }
    }
    void CraftWeapon()
    {
        var newWeapon = Instantiate(weapTemplate, new Vector3(0,0,0), transform.rotation);
        newWeapon.transform.parent = weaponHolder;
        newWeapon.transform.position = weaponHolder.position;

        var newFirstComponent = Instantiate(firstComponent, new Vector3(newWeapon.transform.position.x, newWeapon.transform.position.y + (firstComponent.transform.localScale.y / 2), newWeapon.transform.position.z), player.transform.rotation);
        newFirstComponent.GetComponentInChildren<WeaponComponent>().pos1.transform.position = newWeapon.transform.position;
        newFirstComponent.transform.parent = newWeapon.transform;

        var newSecondComponent = Instantiate(secondComponent, new Vector3(newFirstComponent.transform.GetComponentInChildren<WeaponComponent>().pos2.position.x, newFirstComponent.GetComponentInChildren<WeaponComponent>().pos2.position.y, newFirstComponent.GetComponentInChildren<WeaponComponent>().pos2.position.z), player.transform.rotation);
        secondComponent.GetComponentInChildren<WeaponComponent>().pos1.transform.position = firstComponent.GetComponentInChildren<WeaponComponent>().pos2.transform.position;
        newSecondComponent.transform.parent = newWeapon.transform;

        var newThirdComponent = Instantiate(thirdComponent, new Vector3(newSecondComponent.GetComponentInChildren<WeaponComponent>().pos2.position.x, newSecondComponent.GetComponentInChildren<WeaponComponent>().pos2.position.y, newSecondComponent.GetComponentInChildren<WeaponComponent>().pos2.position.z), player.transform.rotation);
        thirdComponent.GetComponentInChildren<WeaponComponent>().pos1.transform.position = secondComponent.GetComponentInChildren<WeaponComponent>().pos2.transform.position;
        newThirdComponent.transform.parent = newWeapon.transform;

        
        weapSstmAccess.GetCraftedWeapon(newWeapon);
    }



   
}
