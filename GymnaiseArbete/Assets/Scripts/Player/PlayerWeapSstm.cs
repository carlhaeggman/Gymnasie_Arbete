using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapSstm : MonoBehaviour
{
   public GameObject weapon1;
   public GameObject weapon2;
    GameObject crntWeapon;
    GameObject fists;

    private int damage, durability;

    private void Start()
    {
        crntWeapon = weapon1;
        damage = weapon1.GetComponent<Weapon>().damage;
        durability = weapon1.GetComponent<Weapon>().durability;
    }
    private void Update()
    {
        InputManager();
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
        SelectWeapon();
    }
    void DropWeapon()
    {
        if(crntWeapon != fists)
        {
            Instantiate(crntWeapon, transform.position, Quaternion.identity);
            if (crntWeapon = weapon1)
            {
                weapon1 = fists;
                crntWeapon = weapon1;
                print(damage);
            }
            else if (crntWeapon = weapon2)
            {
                weapon2 = fists;
                crntWeapon = weapon2;
                print(damage);
            }
        }
    }
    void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            crntWeapon = weapon1;
            damage = weapon1.GetComponent<Weapon>().damage;
            durability = weapon1.GetComponent<Weapon>().durability;
            print(damage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crntWeapon = weapon2;
            damage = weapon2.GetComponent<Weapon>().damage;
            durability = weapon2.GetComponent<Weapon>().durability;
            print(damage);
        }
    }

    void disposeWeapon()
    {
        if (weapon1.GetComponent<Weapon>().durability <= 0)
        {
            weapon1 = fists;
        }
        if (weapon2.GetComponent<Weapon>().durability <= 0)
        {
            weapon2 = fists;
        }
    }
}
