using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{    

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen obj here
            if (player.HasKitchenObject())
            {
                //Player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                else
                {
                    // Player not holding a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter holding aplate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().
                            GetKitchenObjSO()))
                        {
                            player.GetKitchenObject().SelfDestroy();
                        }
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
}
