using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationHelper : MonoBehaviour
{
    public Enemy enemy;

    public void Attack()
    {
        enemy.MelleAttack();
    }

    public void Shot()
    {
        enemy.Shot();
    }
}
