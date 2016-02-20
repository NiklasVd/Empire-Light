using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeWeaponItem : WeaponItem
{
    public override void Attack(Actor user, Vector2 mousePosition)
    {
        base.Attack(user, mousePosition);
    }
}
