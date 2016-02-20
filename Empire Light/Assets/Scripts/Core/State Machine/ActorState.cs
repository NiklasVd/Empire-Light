using UnityEngine;
using System.Collections;

public abstract class ActorState : StateMachineBehaviour
{
    private Animator myAnimator;
    public Animator MyAnimator
    {
        get { return myAnimator; }
    }

    private int layerIndex;
    public int LayerIndex
    {
        get { return layerIndex; }
    }

    private Actor myActor;
    public Actor MyActor
    {
        get { return myActor; }
    }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myAnimator = animator;
        this.layerIndex = layerIndex;
        myActor = animator.GetComponent<Actor>();

        OnStart(stateInfo);
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnUpdate(stateInfo);
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    protected abstract void OnStart(AnimatorStateInfo stateInfo);
    protected abstract void OnUpdate(AnimatorStateInfo stateInfo);
}
