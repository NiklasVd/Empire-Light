using UnityEngine;
using System.Collections;

public class AttackActorState : ActorState
{
    public const float punchAttackTimeMultiplicator = 0.5f;

    private float attackTime, currentAttackTime;
    private int damage;

    protected override void OnStart(AnimatorStateInfo stateInfo)
    {
        attackTime = MyActor.Attributes.TotalPunchSpeed * punchAttackTimeMultiplicator;
        damage = MyActor.Attributes.TotalPunchDamage;
    }

    protected override void OnUpdate(AnimatorStateInfo stateInfo)
    {
        currentAttackTime += Time.deltaTime * Time.timeScale;
        if (currentAttackTime >= attackTime)
        {
            OnAttack();
        }
    }

    protected virtual void OnAttack()
    {

    }
}
