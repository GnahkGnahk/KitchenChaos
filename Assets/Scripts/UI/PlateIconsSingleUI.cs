using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] Image image;
    public void SetKitchenObjectSo(KitchenObjSO kitchenObjSO)
    {
        image.sprite = kitchenObjSO.sprite;
    }
}
