using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ActorAbilityList
{
    public List<Ability> initializationAbilities;
    public List<Ability> abilities;

    public void Initialize()
    {
        initializationAbilities.ForEach((ia) => { abilities.Add(UnityEngine.Object.Instantiate(ia)); });
    }
}
