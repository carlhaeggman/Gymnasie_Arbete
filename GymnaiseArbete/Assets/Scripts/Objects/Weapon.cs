using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public float accuracy, shootForce, maxAmmo, fireRate, damage;
    List<GameObject> weapCompStats = new List<GameObject>();
    int listLength;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            weapCompStats.Add(child.gameObject);
        }
        listLength = weapCompStats.Count;
        collectInfo();
    }
    void collectInfo()
    {

        for (int i = 0; i < listLength; i++)
        {
            accuracy += weapCompStats[i].GetComponentInChildren<WeaponComponent>().accuracy;
            shootForce += weapCompStats[i].GetComponentInChildren<WeaponComponent>().shootForce;
            maxAmmo += weapCompStats[i].GetComponentInChildren<WeaponComponent>().maxAmmo;
            fireRate += weapCompStats[i].GetComponentInChildren<WeaponComponent>().fireRate;
            damage += weapCompStats[i].GetComponentInChildren<WeaponComponent>().damage;
        }
    }
}
