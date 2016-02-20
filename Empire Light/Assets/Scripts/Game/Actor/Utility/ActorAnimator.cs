using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class ActorAnimator
{
    [SerializeField]
    private Animator animator;

    private List<ActorMovementAnimationItem> movementAnimations = new List<ActorMovementAnimationItem>()
    {
        new ActorMovementAnimationItem(ActorMovementAnimationType.Idle, 0),
        new ActorMovementAnimationItem(ActorMovementAnimationType.Walk, 1),
        new ActorMovementAnimationItem(ActorMovementAnimationType.Run, 2),
        new ActorMovementAnimationItem(ActorMovementAnimationType.Crouch, 3),
        new ActorMovementAnimationItem(ActorMovementAnimationType.Die, 4)
    };

    private List<ActorActionAnimationItem> actionAnimations = new List<ActorActionAnimationItem>()
    {
        new ActorActionAnimationItem(ActorActionAnimationType.Idle, 0),
        new ActorActionAnimationItem(ActorActionAnimationType.Hit, 1),
        new ActorActionAnimationItem(ActorActionAnimationType.Shoot, 2),
        new ActorActionAnimationItem(ActorActionAnimationType.SwingMelee, 3)
    };

    public ActorMovementAnimationItem this[ActorMovementAnimationType movementType]
    {
        get { return FindMovement(movementType); }
    }
    public ActorActionAnimationItem this[ActorActionAnimationType actionType]
    {
        get { return FindAction(actionType); }
    }

    public ActorAnimator()
    {

    }

    public void Initialize()
    {
        SetMovement(ActorMovementAnimationType.Idle);
        SetAction(ActorActionAnimationType.Idle);
    }

    public void SetMovement(ActorMovementAnimationType type)
    {
        animator.SetInteger("Movement Mode", this[type].animationMode);
    }
    public void SetAction(ActorActionAnimationType type)
    {
        animator.SetInteger("Action Mode", this[type].animationMode);
    }

    public ActorMovementAnimationItem FindMovement(ActorMovementAnimationType type)
    {
        return movementAnimations.Find((m) => { if (m.type == type) return true; else return false; });
    }
    public ActorActionAnimationItem FindAction(ActorActionAnimationType type)
    {
        return actionAnimations.Find((a) => { if (a.type == type) return true; else return false; });
    }
}

[Serializable]
public enum ActorMovementAnimationType
{
    Idle,
    Walk,
    Run,
    Crouch,
    Die,
    Jump
}
public enum ActorActionAnimationType
{
    Idle,
    Shoot,
    Hit,
    SwingMelee
}

[Serializable]
public class ActorMovementAnimationItem
{
    public ActorMovementAnimationType type;
    public int animationMode;

    public ActorMovementAnimationItem(ActorMovementAnimationType type, int animationMode)
    {
        this.type = type;
        this.animationMode = animationMode;
    }
}
[Serializable]
public class ActorActionAnimationItem
{
    public ActorActionAnimationType type;
    public int animationMode;

    public ActorActionAnimationItem(ActorActionAnimationType type, int animationMode)
    {
        this.type = type;
        this.animationMode = animationMode;
    }
}