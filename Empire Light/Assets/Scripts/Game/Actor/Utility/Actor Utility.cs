using UnityEngine;
using System;

[Serializable]
public enum ActorTeam
{
    Hero
}
public enum DamageType
{
    Physical,
    Poison,
    Pure
}

[Serializable]
public class SpriteSkeletonItem
{
    public Transform transform;
    public Sprite defaultSprite;
    public SpriteRenderer attachedSpriteRenderer;

    public SpriteSkeletonItem()
    {
    }
}
public enum ActorPopupType
{
    Damage, // When receiving damage (e.g. critical)
    Noticed, // When noticing something (like an enemy)
    Alarmed // When seeing an enemy
}