using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrafter : MonoBehaviour
{
    public GameObject gunFrame, magazine, barrel, stock;
    public GameObject weapTemplate;
    PlayerWeapSstm weapSstmAccess;
    public GameObject player;
    public Transform weaponHolder;

    private void Awake()
    {
        
    }
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
        
        var newWeapon = Instantiate(weapTemplate, new Vector3(0,0,0), Quaternion.identity);

        //Gun frame
        var newGunFrame = Instantiate(gunFrame, new Vector3(newWeapon.transform.position.x, newWeapon.transform.position.y, newWeapon.transform.position.z), Quaternion.identity);
        newGunFrame.GetComponent<WeaponComponent>().positions[0] = newGunFrame.transform.Find("Object").Find("StockPos");
        newGunFrame.GetComponent<WeaponComponent>().positions[1] = newGunFrame.transform.Find("Object").Find("MagazinePos");
        newGunFrame.GetComponent<WeaponComponent>().positions[2] = newGunFrame.transform.Find("Object").Find("BarrelPos");

        //Stock
        var newStock = Instantiate(stock, new Vector3(newGunFrame.GetComponentInChildren<WeaponComponent>().positions[0].localPosition.x, newGunFrame.GetComponentInChildren<WeaponComponent>().positions[0].position.y, newGunFrame.GetComponentInChildren<WeaponComponent>().positions[0].position.z), Quaternion.identity);

        //Magazine
        var newMagazine = Instantiate(magazine, new Vector3(newGunFrame.transform.GetComponentInChildren<WeaponComponent>().positions[1].position.x, newGunFrame.GetComponentInChildren<WeaponComponent>().positions[1].localPosition.y - (magazine.transform.Find("Object").transform.localScale.y/2), newGunFrame.GetComponentInChildren<WeaponComponent>().positions[1].position.z), Quaternion.identity);

        //Barrel
        var newBarrel = Instantiate(barrel, new Vector3(newGunFrame.GetComponentInChildren<WeaponComponent>().positions[2].localPosition.x+(barrel.transform.Find("Object").transform.localScale.x/2), newGunFrame.GetComponentInChildren<WeaponComponent>().positions[2].position.y, newGunFrame.GetComponentInChildren<WeaponComponent>().positions[2].position.z), Quaternion.identity);
        

        newGunFrame.transform.parent = newWeapon.transform;
        newStock.transform.parent = newWeapon.transform;
        newMagazine.transform.parent = newWeapon.transform;
        newBarrel.transform.parent = newWeapon.transform;
        newWeapon.transform.parent = weaponHolder;
        newWeapon.transform.position = weaponHolder.position;

        weapSstmAccess.GetCraftedWeapon(newWeapon);
    }



   
}
