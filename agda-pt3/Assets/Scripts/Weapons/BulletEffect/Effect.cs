﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour{
    public abstract void triggerEffect (GameObject bullet, Collider2D obj, float timeToDie);
}