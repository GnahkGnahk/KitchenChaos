using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] RecipeListSO recipeListSO;

    List<RecipeSO> waitingRecipeSOList;

    [SerializeField] float SpawnRecipeTimerMax; 
    float SpawnRecipeTimer;

    [SerializeField] int waitingRecipeMax;
    int waitingRecipeCount = 0;

    int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();    
    }

    private void Update()
    {
        SpawnRecipeTimer -= Time.deltaTime;
        if (SpawnRecipeTimer <= 0f && waitingRecipeCount < waitingRecipeMax 
            && GameManager.Instance.IsGamePlaying())
        {
            SpawnRecipeTimer = SpawnRecipeTimerMax;

            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[
                UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

            waitingRecipeSOList.Add(waitingRecipeSO);
            waitingRecipeCount++;

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }

    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (var item in waitingRecipeSOList)
        {
            if (item.kitchenObjSOList.Count == plateKitchenObject.GetKitchenObjSOList().Count)
            {
                //  Has the same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (var item1 in item.kitchenObjSOList)
                {
                    //  Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (var item2 in plateKitchenObject.GetKitchenObjSOList())
                    {
                        //  Cycling through all ingredients in the plate
                        if (item2 == item1)
                        {
                            //  Ingredients matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //  This recipe ingredient was not found on the plate
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    //  Player deliverd the correct recipe
                    waitingRecipeSOList.Remove(item);

                    Debug.Log("Player deliverd the correct recipe");
                    waitingRecipeCount--;

                    successfulRecipesAmount++;

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //  No matches found.
        //  Player did not deliverd the correct recipe

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }

}
