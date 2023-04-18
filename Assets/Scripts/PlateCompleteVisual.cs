using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchanObjSO_GameObject
    {
        public KitchenObjSO kitchenObjSO;
        public GameObject gameObject;
    }

    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchanObjSO_GameObject> kitchanObjSO_GameObjectList;


    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, 
        PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var item in kitchanObjSO_GameObjectList)
        {
            if (item.kitchenObjSO == e.kitchenObjSO)
            {
                item.gameObject.SetActive(true);
                return;
            }
        }
    }
}
