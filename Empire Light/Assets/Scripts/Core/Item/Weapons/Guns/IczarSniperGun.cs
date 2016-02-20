using UnityEngine;
using System.Collections;
using CaseoMatic.Util;

public class IczarSniperGun : GunWeaponItem
{
    private static RNG criticalDamageRng = new RNG();

    public int criticalDamageChancePercentage;
    public float criticalDamageMultiplicator;

    public override void OnHit(Actor user, Actor target, int damage)
    {
        if (criticalDamageRng.Determine((float)criticalDamageChancePercentage))
        {
            user.DealDamage(target, Mathf.RoundToInt(damage * criticalDamageMultiplicator), DamageType.Physical);
            target.ShowPopup(ActorPopupType.Damage);
        }

        base.OnHit(user, target, damage);
    }
}
