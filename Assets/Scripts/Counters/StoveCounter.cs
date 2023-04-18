using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgesss
{
    public event EventHandler<IHasProgesss.OnprogressChangedEventArgs> OnprogressChanged;
    public class OnprogressChangedEventArgs : EventArgs {
        public float progressNomalized;
    }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle, Fring, Fried, Burned,
    }

    [SerializeField] FryingRecipeSO[] FryingRecipeSOArray;

    State state;
    float fryingTimer;
    int stateOfKitchen;
    FryingRecipeSO fryingRecipeSO;

    float scaleForFry = 1f;
    [SerializeField][Range(1,2)] float scaleForFried = 1.5f;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            if (stateOfKitchen < 3)
            {
                switch (state)
                {
                    case State.Idle:
                        break;
                    case State.Fring:
                        ProgressBarEvent(scaleForFry);
                        fryingTimer += Time.deltaTime;
                        if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                        {
                            GetKitchenObject().SelfDestroy();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output_1, this);
                            state = State.Fried;
                            fryingTimer = 0f;

                            OnStoveEvent();
                        }
                        break;
                    case State.Fried:
                        ProgressBarEvent(scaleForFried);
                        fryingTimer += Time.deltaTime;
                        if (fryingTimer >= fryingRecipeSO.fryingTimerMax * scaleForFried)
                        {
                            GetKitchenObject().SelfDestroy();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output_2, this);
                            state = State.Burned;

                            OnStoveEvent();
                        }
                        break;
                    case State.Burned:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen obj here
            if (player.HasKitchenObject())
            {
                //Player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);

                stateOfKitchen = GetStateFryingRecipeSOWithInput(
                    GetKitchenObject().GetKitchenObjSO());

                if (stateOfKitchen < 3)
                {
                    switch (stateOfKitchen)
                    {
                        case 0:
                            state = State.Fring;
                            HandleFrying();
                            break;
                        case 1:
                            state = State.Fried;
                            HandleFrying();
                            break;
                        case 2:
                            state = State.Burned;
                            break;
                        default:
                            Debug.Log("default");
                            break;
                    }
                }
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

                        state = State.Idle;

                        OnStoveEvent();

                        fryingTimer = 0;
                        ProgressBarEvent(1f);
                    }
                }
            }
            else
            {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;

                OnStoveEvent();

                fryingTimer = 0;
                ProgressBarEvent(1f);
            }
        }
    }

/*    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(
                    GetKitchenObject().GetKitchenObjSO());

            if (fryingRecipeSO != null)
            {
                // KitchenObj can be fry                

            }


        }
    }*/

    bool HasFryingRecipeSO(KitchenObjSO inputKitchenObjSO)
    {
        return GetFryingRecipeSOWithInput(inputKitchenObjSO) != null;
    }

    private KitchenObjSO GetOutputForInput(KitchenObjSO inputKitchenObjSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output_1;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjSO inputKitchenObjSO)
    {
        foreach (var item in FryingRecipeSOArray)
        {
            if (item.input == inputKitchenObjSO)
            {
                return item;
            }
            if (item.output_1 == inputKitchenObjSO)
            {
                return item;
            }
        }
        return null;
    }
    private int GetStateFryingRecipeSOWithInput(KitchenObjSO inputKitchenObjSO)
    {
        foreach (var item in FryingRecipeSOArray)
        {
            if (item.input == inputKitchenObjSO)
            {
                return 0;
            }
            if (item.output_1 == inputKitchenObjSO)
            {
                return 1;
            }
            if (item.output_2 == inputKitchenObjSO)
            {
                return 2;
            }
        }
        return 3;
    }
    void HandleFrying()
    {
        fryingRecipeSO = GetFryingRecipeSOWithInput(
                    GetKitchenObject().GetKitchenObjSO());
        //state = State.Fring;
        fryingTimer = 0f;

        //ProgressBarEvent(scaleForFry);
        OnStoveEvent();
    }

    void OnStoveEvent()
    {
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });
    }
    void ProgressBarEvent(float value)
    {
        OnprogressChanged?.Invoke(this, new IHasProgesss.OnprogressChangedEventArgs
        {
            progressNomalized =
                    (float)fryingTimer / (fryingRecipeSO.fryingTimerMax * value)
        }) ;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }

    public bool IsFring()
    {
        return state == State.Fring;
    }
}
