using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgesss
{
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgesss.OnprogressChangedEventArgs> OnprogressChanged;
    public class OnprogressChangedEventArgs : EventArgs
    {
        public float progressNomalized;
    }

    public event EventHandler OnCut;

    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;

    int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen obj here
            if (player.HasKitchenObject())
            {
                //Player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);                

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(
                    GetKitchenObject().GetKitchenObjSO());
                if (cuttingRecipeSO == null)
                {
                    return;
                }

                cuttingProgress = 0;
                OnprogressChanged?.Invoke(this, new IHasProgesss.OnprogressChangedEventArgs
                {
                    progressNomalized = 
                    (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a kitchen obj here
            if (player.HasKitchenObject())
            {
                //Player is carrying sth
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //  Player holding a plate                     
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjSO()))
                    {
                        GetKitchenObject().SelfDestroy();
                    }
                }
            }
            else
            {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(
                    GetKitchenObject().GetKitchenObjSO());

            if (cuttingRecipeSO != null)                 
            {
                // kitchenObj can be cut

                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);

                OnprogressChanged?.Invoke(this, new IHasProgesss.OnprogressChangedEventArgs
                {
                    progressNomalized =
                    (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });

                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjSO outputKitchenObjSO = GetOutputForInput(
                    GetKitchenObject().GetKitchenObjSO());

                    GetKitchenObject().SelfDestroy();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjSO, this);
                }
            }
            

        }
    }

    bool HasCuttingRecipeSO(KitchenObjSO inputKitchenObjSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjSO) != null;
    }

    private KitchenObjSO GetOutputForInput(KitchenObjSO inputKitchenObjSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjSO inputKitchenObjSO)
    {
        foreach (var item in cuttingRecipeSOArray)
        {
            if (item.input == inputKitchenObjSO)
            {
                return item;
            }
        }
        return null;
    }

}
