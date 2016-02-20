using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NPCActorControl : ActorControl
{
    public const float enemyCheckRepeatTime = 0.5f;

    public NPCBehaviourMode behaviourMode;
    public float daySightRange, nightSightRange;

    public NPCActorAttitudes attitudes;
    public int attitudeRandomness;

    private float currentRecognitionEventTime;

    private SpriteRenderer spriteRenderer;

    public Actor targetEnemy { get; private set; }

    private NPCRecognitionEvent currentRecognitionEvent;
    public NPCRecognitionEvent CurrentRecognitionEvent
    {
        get { return currentRecognitionEvent; }
    }

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GenerateAttitudes();

        base.Awake();
    }

    protected override void Update()
    {
    }

    public void RecognizeEvent(NPCRecognitionEvent recognitionEvent)
    {
        currentRecognitionEvent = recognitionEvent;
    }

    //private void CheckEnemies()
    //{
    //    if (behaviourMode == NPCBehaviourMode.Peaceful || behaviourMode == NPCBehaviourMode.Normal)
    //        return;

    //    var colliders = Physics2D.OverlapCircleAll(transform.position, daySightRange, spriteRenderer.sortingLayerID); // TODO: Use night sight range if night
    //    var enemyActorsNear = new List<Actor>();
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.CompareTag("Actor"))
    //        {
    //            enemyActorsNear.Add(collider.GetComponent<Actor>());
    //        }
    //    }
    //}

    private void GenerateAttitudes()
    {
        attitudes.items.ForEach((a) => { a.value += UnityEngine.Random.Range(-attitudeRandomness, attitudeRandomness); });
    }
}

[Serializable]
public enum NPCBehaviourMode
{
    Peaceful,
    Normal,
    Aggressive,
    Crazy
}

[Serializable]
public enum NPCRecognitionEventType
{
    HeardSound = 0,
    SawCorpse = 1,
    SawEnemy = 2,
    ReceivedDamage = 3
}
public class NPCRecognitionEvent
{
    public readonly NPCRecognitionEventType eventType;
    public readonly Vector2 atPosition;

    // Heard sound
    public readonly float heardSoundVolume;
    public readonly bool heardSoundIsShot;

    // Saw enemy
    public Actor sawEnemyActor;
    public readonly bool sawEnemyInLight;

    // Saw corpse
    public Actor sawCorpseActor;
    public readonly bool sawCorpseInLight;

    // Received damage
    public readonly float receivedDamageAmount;

    public NPCRecognitionEvent(NPCRecognitionEventType eventType, Vector2 atPosition,
        float heardSoundVolume = 0, bool heardSoundIsShot = false,
        bool sawEnemyInLight = false, Actor sawEnemyActor = null,
        bool sawCorpseInLight = false, Actor sawCorpseActor = null,
        float receivedDamageAmount = 0)
    {
        this.eventType = eventType;
        this.atPosition = atPosition;

        this.heardSoundVolume = heardSoundVolume;
        this.heardSoundIsShot = heardSoundIsShot;

        this.sawEnemyInLight = sawEnemyInLight;
        this.sawEnemyActor = sawEnemyActor;

        this.sawCorpseInLight = sawCorpseInLight;
        this.sawCorpseActor = sawCorpseActor;

        this.receivedDamageAmount = receivedDamageAmount;
    }
}