using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public int damage, durability;
    List<GameObject> weapCompStats = new List<GameObject>();
    int listLength;

    private void Start()
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
            damage += weapCompStats[i].GetComponentInChildren<WeaponComponent>().damage;
        }
        durability = weapCompStats[0].GetComponentInChildren<WeaponComponent>().durability;
        //Debug.Log(damage +" "+ transform.gameObject.name);
    }
}
