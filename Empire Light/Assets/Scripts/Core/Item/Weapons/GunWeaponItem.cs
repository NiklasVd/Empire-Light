using UnityEngine;
using System.Linq;

public class GunWeaponItem : WeaponItem
{
    public Transform weaponBulletHole;

    public int damage;
    public float range;
    public bool perforates;

    public int magazineLength, currentBulletsInMagazine, bullets;

    public override void Attack(Actor user, Vector2 mousePosition)
    {
        if (bullets <= 0)
            return;

        if (perforates)
        {
            var targets = Physics2D.RaycastAll(user.transform.position, mousePosition).OrderBy(h => h.distance);
            var currentDamage = damage;

            foreach (var target in targets)
            {
                if (target.rigidbody != null)
                {
                    HitTarget(target.rigidbody, user.transform.position, target, currentDamage / 5f);
                }
                if (target.collider.CompareTag("Actor"))
                {
                    var targetActor = target.collider.GetComponent<Actor>();

                    user.DealDamage(targetActor, currentDamage, DamageType.Physical);
                    OnHit(user, targetActor, currentDamage);
                }
                currentDamage = Mathf.RoundToInt(currentDamage * 0.1f); // Dont make the weapon weakness factor hardcoded!
                if (currentDamage <= damage / 2) // The bullet is not strong enough anymore to perforate its next target
                    break;
            }
        }
        else
        {
            var target = Physics2D.Raycast(user.transform.position, mousePosition);
            if (target.rigidbody != null)
            {
                HitTarget(target.rigidbody, user.transform.position, target, damage / 5f);
            }
            if (target.collider.CompareTag("Actor"))
            {
                var targetActor = target.collider.GetComponent<Actor>();

                user.DealDamage(targetActor, damage, DamageType.Physical);
                OnHit(user, targetActor, damage);
            }
        }

        currentBulletsInMagazine -= 1;
        base.Attack(user, mousePosition);
    }

    public void Reload()
    {
        var reloadedBullets = bullets - currentBulletsInMagazine >= 0 ? bullets - currentBulletsInMagazine : bullets;

        currentBulletsInMagazine += reloadedBullets;
        bullets -= reloadedBullets;
    }

    private void HitTarget(Rigidbody2D rigidbody, Vector2 from, RaycastHit2D hit, float strength)
    {
        rigidbody.AddForce((hit.point - from).normalized * strength, ForceMode2D.Impulse);
    }
}
