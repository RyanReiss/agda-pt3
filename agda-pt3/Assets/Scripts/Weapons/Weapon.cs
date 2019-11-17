using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float fireRate;
    public Transform spawnPos;

    public abstract void Attack();
}
