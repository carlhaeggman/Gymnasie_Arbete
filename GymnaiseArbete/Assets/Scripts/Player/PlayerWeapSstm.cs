using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapSstm : MonoBehaviour
{
   public GameObject weapon1;
   public GameObject weapon2;
   GameObject crntWeapon;
   public GameObject fists;

   public Transform weaponPos;
   public Transform rayCastPos;
   
   GameObject newWeap;
   public GameObject weaponStorage;
   private bool usingFirstWeapon;
   private bool usingSecondWeapon;

   public TrailRenderer projectileTrailRndr;
    private void Start()
    {
        usingFirstWeapon = true;
        usingSecondWeapon = false;
        newWeap = fists;
        crntWeapon = weapon1;
        UpdateInfo(weapon1);
  
    }
    private void Update()
    {
        InputManager();
        PickUpWeap();
    }
    void InputManager()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            DropWeapon();
        }
      
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            crntWeapon = weapon1;
            UpdateInfo(weapon1);
            usingFirstWeapon = true;
            usingSecondWeapon = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crntWeapon = weapon2;
            UpdateInfo(weapon2);
            usingFirstWeapon = false;
            usingSecondWeapon = true;
        }
    }

    void PickUpWeap()
    {
        float distance = 5f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, distance))
        {
            if(hit.transform.gameObject.CompareTag("CanGrab") && Input.GetKeyUp(KeyCode.E))
            {
                newWeap = hit.transform.parent.transform.parent.transform.parent.gameObject;
                DropWeapon();
                if (crntWeapon = weapon1)
                {
                    if(weapon1 != fists)
                    {
                        DropWeapon();
                    }
                    weapon1 = newWeap;
                    crntWeapon = weapon1;
                    crntWeapon.transform.SetParent(transform);
                    crntWeapon.transform.position = weaponPos.position;

                    UpdateInfo(weapon1);
                }
                else if (crntWeapon = weapon2)
                {
                    if (weapon2 != fists)
                    {
                        DropWeapon();
                    }
                    weapon2 = newWeap;
                    crntWeapon = weapon2;
                    UpdateInfo(weapon2);
                }
            }
        }
    }

    void DropWeapon()
    {
        if(crntWeapon != fists)
        {
            crntWeapon.transform.position = transform.position + (transform.forward * 2);
            crntWeapon.transform.parent = weaponStorage.transform;

            if (crntWeapon == weapon1 && weapon1 != fists)
            {
                weapon1 = fists;
                crntWeapon = fists;
                UpdateInfo(weapon1);
            }
            else if (crntWeapon == weapon2 && weapon1 != fists)
            {
                weapon2 = fists;
                crntWeapon = fists;
                UpdateInfo(weapon2);
            }
        }
    }

    void UpdateInfo(GameObject weapon)
    {
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        PlayerShoot playerShoot = gameObject.GetComponent<PlayerShoot>();

        playerShoot.damage = weaponScript.damage;
        playerShoot.bulletDrop = weaponScript.accuracy;
        playerShoot.maxAmmo = weaponScript.maxAmmo;
        playerShoot.fireRate = weaponScript.fireRate;
        
        return;
    }

    public void GetCraftedWeapon(GameObject craftedWeapon)
    {
        //DropWeapon();
        if (usingFirstWeapon == true)
        {
            weapon1 = craftedWeapon;
            crntWeapon = weapon1;
            UpdateInfo(weapon1);
        }
        else if(usingSecondWeapon == true)
        {
            weapon2 = craftedWeapon;
            crntWeapon = weapon2;
            UpdateInfo(weapon2);
        }
    }

}
