using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentType {Stock, Frame, Barrel, Magazine}
public class WeaponComponent : MonoBehaviour
{
    public ComponentType thisComponentType;
    public int damage,durability;
    public float accuracy, shootForce, ammo;
    public Transform pos1, pos2;
    private void Awake()
    {
        
    }
    private void setComponentType()
    {
       
    }
    private void Stock()
    {
     
    }
    private void Magazine()
    {

    }
    private void Barrel()
    {

    }

}
