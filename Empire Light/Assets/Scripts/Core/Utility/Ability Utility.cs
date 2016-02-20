using UnityEngine;
using System;

[Serializable, Flags]
public enum AbilityTargetType
{
    None = 1 << 0,

    Position = 1 << 1,
    Direction = 1 << 2,

    Actor = 1 << 3,
    FriendlyActor = Actor | 1 << 4,
    EnemyActor = Actor | 1 << 5,
}
public enum AbilityTryCastResult
{
    Success,
    EmptyCastInfo,
    Cooldown,
    ExceedingRange,
    WrongActor,
    NoActor
}

public struct AbilityCastInfo
{
    public readonly Vector2 targetPosition;
    public readonly Actor targetActor;

    public bool IsEmpty { get { return (targetPosition == default(Vector2) && targetActor == null); } }

    public AbilityCastInfo(Vector2 targetPosition = default(Vector2), Actor targetActor = null)
    {
        this.targetPosition = targetPosition;
        this.targetActor = targetActor;
    }
    public override string ToString()
    {
        return (targetPosition != default(Vector2) ? targetPosition.ToString() :
            (targetActor != null ? targetActor.EntityName : "No Target"));
    }
}
