using UnityEngine;
using System.Collections;
using System;

public class Actor : Entity
{
    public const float popupShowTime = 1.5f;

    private ActorTeam team;
    public ActorTeam Team
    {
        get { return team; }
    }

    private WeaponItem currentWeapon;
    public WeaponItem CurrentWeapon
    {
        get { return currentWeapon; }
    }

    // Resources
    [SerializeField]
    private ActorAttributes attributes;
    public ActorAttributes Attributes
    {
        get { return attributes; }
    }

    [SerializeField]
    private ActorArmorEquipment equipment;
    public ActorArmorEquipment Equipment
    {
        get { return equipment; }
    }

    [SerializeField]
    private ActorInventory inventory;
    public ActorInventory Inventory
    {
        get { return inventory; }
    }

    [SerializeField]
    private ActorAbilityList abilityList;
    public ActorAbilityList AbilityList
    {
        get { return abilityList; }
    }

    public ActorAnimator animator;

    // States
    [SerializeField]
    private bool isImmortal, isParalyzed;
    public bool IsImmortal
    {
        get { return isImmortal; }
    }
    public bool IsParalyzed
    {
        get { return isParalyzed; }
    }

    // References
    public Transform popupObjectTransform;

    public SpriteSkeletonItem headAttacher, bodyAttacher, handAttacher,
        leftLegAttacher, rightLegAttacher,
        leftFootAttacher, rightFootAttacher;

    private bool isPopupShown;

    public void CastAbility(Ability ability, AbilityCastInfo castInfo)
    {
        var tryCastResult = ability.Cast(this, castInfo);
        Debug.Log("Tried casting " + ability.abilityName + " to " + castInfo + ": " + tryCastResult);
    }

    public void ConsumeItem(ConsumableItem consumable)
    {
        if (inventory.items.Contains(consumable))
        {
            consumable.Consume(this);
            if (consumable.removeOnConsume)
                inventory.RemoveItem(consumable);
        }
    }
    public void UseWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Attack(this, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public void SetWeapon(WeaponItem weapon)
    {
        if (weapon == null)
        {
            inventory.AddExistingItem(currentWeapon);
            currentWeapon = null;
        }
        else if (inventory.items.Contains(weapon))
        {
            inventory.RemoveItem(weapon);
            currentWeapon = weapon;
        }

        AttachSprite(handAttacher, currentWeapon != null ? currentWeapon.weaponSprite : handAttacher.defaultSprite);
    }

    public void EquipItem(ArmorItem item)
    {
        if (inventory.items.Contains(item))
        {
            if (equipment[item.armorType].item != null)
                UnequipItem(item.armorType);

            inventory.RemoveItem(item);
            equipment.EquipItem(item.armorType, item);

            switch (item.armorType)
            {
                case ActorArmorType.Head:
                    AttachSprite(headAttacher, item.equippedSprite);
                    break;
                case ActorArmorType.Body:
                    AttachSprite(bodyAttacher, item.equippedSprite);
                    break;
                //case ActorEquipmentType.Legs:
                //    AttachSprite(leftLegAttacher, item.equippedSprite);
                //    AttachSprite(rightFootAttacher, item.equippedSprite);
                //    break;
                //case ActorEquipmentType.Foots:
                //    AttachSprite(leftFootAttacher, item.equippedSprite);
                //    AttachSprite(rightFootAttacher, item.equippedSprite);
                //    break;
            }
        }
    }
    public void UnequipItem(ActorArmorType type)
    {
        var equipItem = equipment[type];
        if (equipItem != null)
        {
            inventory.AddExistingItem(equipItem.item); // Does this work? Because the reference is now set to null or...?
            equipItem.item = null;
        }
    }

    public void ShowPopup(ActorPopupType popupType)
    {
        if (isPopupShown)
            return;

        GameObject popupObject = gameObject;
        switch (popupType)
        {
            case ActorPopupType.Damage:
                popupObject = GameObjectHelper.Instance.actorPopupDamage;
                break;
            case ActorPopupType.Noticed:
                popupObject = GameObjectHelper.Instance.actorPopupNoticed;
                break;
            case ActorPopupType.Alarmed:
                popupObject = GameObjectHelper.Instance.actorPopupAlarmed;
                break;
        }

        StartCoroutine(ShowPopupCoroutine(popupObject));
    }

    public void DealDamage(Actor target, int amount, DamageType type)
    {
        target.ReceiveDamage(this, amount, type);
    }

    protected internal void ReceiveDamage(Actor damageDealer, int amount, DamageType type)
    {
        if (isImmortal)
            return;

        switch (type)
        {
            case DamageType.Physical:
                amount -= attributes.TotalPhysicalResistance;
                break;
            case DamageType.Poison:
                amount -= attributes.TotalPoisonResistance;
                break;
            case DamageType.Pure:
                break; // Do nothing
        }

        if (attributes.CurrentHealth == 0)
            OnDie();
    }

    protected virtual void OnDie()
    {
    }

    public override void OnWriteSaveStream(ObjectSaveStream stream)
    {
        // TODO: Save the current weapon

        // TODO: Write through implemented write methods
        stream.Write("attr", attributes);
        stream.Write("abl", abilityList);
        stream.Write("eqp", equipment);
        stream.Write("inv", inventory);
        // Animator must not be saved

        base.OnWriteSaveStream(stream);
    }
    public override void OnReadSaveStream(ObjectSaveStream stream)
    {
        // TODO: Equip current weapon

        // TODO: Read through implemented read methods
        attributes = stream.Read<ActorAttributes>("attr");
        abilityList = stream.Read<ActorAbilityList>("abl");
        equipment = stream.Read<ActorArmorEquipment>("eqp");
        inventory = stream.Read<ActorInventory>("inv");

        base.OnReadSaveStream(stream);
    }

    protected override void Awake()
    {
        attributes.Initialize();
        abilityList.Initialize();
        inventory.Initialize();
        animator.Initialize();

        base.Awake();
    }
    protected override void Update()
    {
        abilityList.abilities.ForEach((a) => { a.Update(); });
        base.Update();
    }

    private void AttachSprite(SpriteSkeletonItem spriteSkeletonItem, Sprite sprite)
    {
        spriteSkeletonItem.attachedSpriteRenderer.sprite = sprite != null ? sprite : spriteSkeletonItem.defaultSprite;
    }
    private IEnumerator ShowPopupCoroutine(GameObject popupObject)
    {
        isPopupShown = true;

        var instantiatedPopupObject = Instantiate(popupObject);
        instantiatedPopupObject.transform.parent = transform;
        instantiatedPopupObject.transform.position = instantiatedPopupObject.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(popupShowTime);

        Destroy(instantiatedPopupObject);
        isPopupShown = false;
    }
}
