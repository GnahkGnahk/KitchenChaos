using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjSO kitchenObjSO;
    }

    [SerializeField] List<KitchenObjSO>  validKitchenObjSOList;

    List<KitchenObjSO> kitchenObjSOList;

    private void Awake()
    {
        kitchenObjSOList = new List<KitchenObjSO>();
    }
    public bool TryAddIngredient(KitchenObjSO kitchenObjSO)
    {
        if (kitchenObjSOList.Contains(kitchenObjSO)
            || !validKitchenObjSOList.Contains(kitchenObjSO))
        {
            // Already have this type
            return false;
        }
        else
        {
            kitchenObjSOList.Add(kitchenObjSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjSO = kitchenObjSO
            });
            return true;
        }

    }

    public List<KitchenObjSO> GetKitchenObjSOList()
    {
        return kitchenObjSOList;
    }
}
