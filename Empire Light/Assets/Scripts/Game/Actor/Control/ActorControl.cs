using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Actor))]
public abstract class ActorControl : MonoBehaviour
{
    protected Actor Actor
    {
        get;
        private set;
    }

    private Rigidbody2D rigidBody;

    protected virtual void Awake()
    {
        Actor = GetComponent<Actor>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    protected void Move(float axisValue, bool run)
    {
        var velocity = rigidBody.velocity;
        velocity.x += Actor.attributes.TotalMovementSpeed * (run ? 2 : 1)
            * axisValue * Time.deltaTime; // Do NOT hardcode the 2 (run multiplicator)
        rigidBody.velocity = velocity;
    }
    protected void Jump()
    {
        rigidBody.AddForce(-Actor.transform.up, ForceMode2D.Impulse);
    }
}
