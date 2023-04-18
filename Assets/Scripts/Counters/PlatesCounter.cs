using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;


    [SerializeField]
    float spawnPlatesTimerMax;
    [SerializeField]
    float spawnPlatesAmountMax;
    [SerializeField]
    KitchenObjSO plateKitchenObjSO;

    float spawnPlatesTimer;
    float spawnPlatesAmount;

    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if (spawnPlatesTimer > spawnPlatesTimerMax 
            && GameManager.Instance.IsGamePlaying())
        {
            spawnPlatesTimer = 0f;

            if (spawnPlatesAmount < spawnPlatesAmountMax)
            {
                spawnPlatesAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is emplty handed
            if (spawnPlatesAmount > 0)
            {
                // There's at least 1 plate on couner
                spawnPlatesAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
