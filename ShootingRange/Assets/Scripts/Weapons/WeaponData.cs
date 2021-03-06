﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public float fireRate;
    public float accuracy;
    public float reloadSpeed;
    public float kickback;
    public float shotRotation;
    public int maxAmmo;
    public Material muzzleFlashMat;
}
