using UnityEngine;
using System.Collections;

public class PlayerActorControl : ActorControl
{
    protected override void Update()
    {
        ProcessMovementInput();
        ProcessUtilityInput();

        base.Update();
    }

    private void ProcessMovementInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        //var vertical = Input.GetAxis("Vertical");

        // Apply walking movement
        Move(horizontal, Input.GetKeyDown(KeyCode.LeftShift));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void ProcessUtilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Actor.SetWeapon(Actor.inventory.items[0] as WeaponItem);
        }
    }
}
