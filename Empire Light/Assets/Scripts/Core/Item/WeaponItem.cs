using UnityEngine;
using System.Collections;

public class WeaponItem : Item
{
    public Sprite weaponSprite;
    public float attackCooldown;
    public float soundRange;

    [HideInInspector]
    public float currentAttackCooldown;

    public virtual void Attack(Actor user, Vector2 mousePosition)
    {

    }

    public virtual void OnHit(Actor user, Actor target, int damage)
    {

    }
}
