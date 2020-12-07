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
   GameObject newWeap;

    private void Start()
    {
        newWeap = fists;
        crntWeapon = weapon1;
        damage = weapon1.GetComponent<Weapon>().damage;
        durability = weapon1.GetComponent<Weapon>().durability;
    }
    private void Update()
    {
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
            Debug.Log(fists.GetComponent<Weapon>().damage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crntWeapon = weapon2;
            UpdateInfo(weapon2);
        }
    }

    void PickUpWeap()
    {
        float distance = 5f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit, distance))
        {
            if(hit.transform.tag == "CanGrab" && Input.GetKeyUp(KeyCode.E))
            {
                newWeap = hit.transform.gameObject;
                DropWeapon();
                if (crntWeapon = weapon1)
                {
                    weapon1 = newWeap;
                    crntWeapon = weapon1;
                    crntWeapon.transform.SetParent(transform);
                    crntWeapon.transform.position = weaponPos.position;

                    UpdateInfo(weapon1);
                }
                else if (crntWeapon = weapon2)
                {
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
            Instantiate(crntWeapon, transform.position+(transform.forward * 2), Quaternion.identity);
            Destroy(weapon1);
            Debug.Log("Dropped");
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
            Destroy(weapon1);
            weapon1 = fists;
            UpdateInfo(weapon1);
        }
        if (weapon2.GetComponent<Weapon>().durability <= 0)
        {
            Destroy(weapon2);
            weapon2 = fists;
            UpdateInfo(weapon2);
        }
    }

    void Attack()
    {
        
    }

    void UpdateInfo(GameObject weapon)
    {
        damage = weapon.GetComponent<Weapon>().damage;
        durability = weapon.GetComponent<Weapon>().durability;
        return;
    }
}
