using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjSO input;
    public KitchenObjSO output_1;
    public KitchenObjSO output_2;
    public float fryingTimerMax;
}
