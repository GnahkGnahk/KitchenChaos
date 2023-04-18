using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]
    KitchenObjSO kitchenObjSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

            KitchenObject.SpawnKitchenObject(kitchenObjSO, player);
        }
    }
    public override void InteractAlternate(Player player)
    {
        Debug.LogError("ContainerCounter.InteractAlternate");
    }
}
