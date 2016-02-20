using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

[CreateAssetMenu(fileName = "New Ability", menuName = "Game Resources/Ability")]
public abstract class Ability : ScriptableObject
{
    public int resourceId;
    public string abilityName, description;

    public AbilityTargetType targetType;
    public float maxCastRange, cooldown, currentCooldown;

    public virtual AbilityTryCastResult Cast(Actor caster, AbilityCastInfo castInfo)
    {
        var tryCastResult = CanCast(caster, castInfo);
        if (tryCastResult == AbilityTryCastResult.Success)
        {
            OnCast(caster, castInfo);
            currentCooldown = cooldown;
        }

        return tryCastResult;
    }

    protected virtual AbilityTryCastResult CanCast(Actor caster, AbilityCastInfo castInfo)
    {
        // Caster
        if (currentCooldown != 0)
        {
            return AbilityTryCastResult.Cooldown;
        }

        // Cast info
        if (!HasTargetTypeFlags(AbilityTargetType.None) && castInfo.IsEmpty)
        {
            return AbilityTryCastResult.EmptyCastInfo;
        }
        if (HasTargetTypeFlags(AbilityTargetType.Position) && Vector2.Distance(caster.transform.position, castInfo.targetPosition) > maxCastRange)
        {
            return AbilityTryCastResult.ExceedingRange;
        }

        if (HasTargetTypeFlags(AbilityTargetType.Actor) && castInfo.targetActor == null)
        {
            return AbilityTryCastResult.NoActor;
        }
        if ((HasTargetTypeFlags(AbilityTargetType.FriendlyActor) && castInfo.targetActor.Team != caster.Team) ||
            HasTargetTypeFlags(AbilityTargetType.EnemyActor) && castInfo.targetActor.Team == caster.Team)
        {
            return AbilityTryCastResult.WrongActor;
        }

        return AbilityTryCastResult.Success;
    }
    protected abstract void OnCast(Actor caster, AbilityCastInfo castInfo);

    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
            }
        }
    }

    private bool HasTargetTypeFlags(AbilityTargetType target)
    {
        return (targetType & target) == target;
    }
}
