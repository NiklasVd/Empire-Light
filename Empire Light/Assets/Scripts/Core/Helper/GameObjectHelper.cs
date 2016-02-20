using UnityEngine;
using System.Collections;

public class GameObjectHelper : MonoBehaviour, IHelper<GameObjectHelper>
{
    public static GameObjectHelper Instance { get; private set; }
    public GameObject actorPopupDamage, actorPopupNoticed, actorPopupAlarmed;

    void Awake()
    {
        Instance = this;
    }
}
