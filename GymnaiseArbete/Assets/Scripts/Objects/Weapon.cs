using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage, durability;
    List<GameObject> weapCompStats = new List<GameObject>();
    int listLength;
    bool hasCollected;

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
        for (int i = 0; i-1 < listLength; i++)
        {
            damage += weapCompStats[0].GetComponent<WeaponComponent>().damage;
        }
        durability = weapCompStats[0].GetComponent<WeaponComponent>().durability;
    }
}
