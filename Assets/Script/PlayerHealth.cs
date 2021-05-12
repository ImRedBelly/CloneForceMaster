using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    
    public Action OnDeath;

    int health = 3;

    private void Awake()
    {
        instance = this;
    }
    public void Damage()
    {
        health--;

        if (health <= 0)
            OnDeath();
    }
}
