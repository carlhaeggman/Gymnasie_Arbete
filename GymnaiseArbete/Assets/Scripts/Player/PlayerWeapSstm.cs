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

   private int damage, durability;
   private float range, accuracy, shootForce, ammo;
    private GameObject cameraHolder;

   GameObject newWeap;
   public GameObject weaponStorage;
   private bool usingFirstWeapon;
   private bool usingSecondWeapon;



    private void Start()
    {
        cameraHolder = GameObject.Find("CharacterCamera");
        usingFirstWeapon = true;
        usingSecondWeapon = false;
        newWeap = fists;
        crntWeapon = weapon1;
        UpdateInfo(weapon1);
        range = 9999.0f;
    }
    private void Update()
    {
        Attack();
        InputManager();
        PickUpWeap();
        if(durability <= 0)
        {
          disposeWeapon();
        }
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

    void disposeWeapon()
    {
        if (weapon1.GetComponent<Weapon>().durability <= 0)
        {
            weapon1.SetActive(false);
            weapon1.name = weapon1.name + " - " + "DESTROYED";
            weapon1.transform.parent = weaponStorage.transform;
            weapon1 = fists;
            UpdateInfo(weapon1);
        }
        if (weapon2.GetComponent<Weapon>().durability <= 0)
        {
            weapon2.SetActive(false);
            weapon2.name = weapon2.name + " - " + "DESTROYED";
            weapon2.transform.parent = weaponStorage.transform;
            weapon2 = fists;
            UpdateInfo(weapon2);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            
           if (Physics.Raycast(cameraHolder.GetComponent<Camera>().transform.position, cameraHolder.GetComponent<Camera>().transform.forward, out hit, range)){
                Stats stats = hit.transform.gameObject.GetComponent<Stats>();
                if (stats != null)
                {
                    stats.TakeDamage(damage);
                }
           }
        }
    }

    void UpdateInfo(GameObject weapon)
    {
        damage = weapon.GetComponent<Weapon>().damage;
        durability = weapon.GetComponent<Weapon>().durability;
        return;
    }

    public void GetCraftedWeapon(GameObject craftedWeapon)
    {
        DropWeapon();
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
