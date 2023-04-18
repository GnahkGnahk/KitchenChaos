using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler OnPickSomething;

    public event EventHandler<OnselectedCounterchargedEventArgs> OnSelectedCountercharged;
    public  class OnselectedCounterchargedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField]
    float moveSpeed, rotateSpeed;
    [SerializeField]
    LayerMask counterLayerMask;

    [SerializeField]
    Transform kitchenObjectHoldPoint;


    Vector3 lastInteractDir;
    Vector2 moveDir, interactDir;
    Vector3 moveDirV3, interactDirV3;
    bool isWalking;
    GameInput gameInput;

    float playerRadius = .7f;
    float playerHeight = 2f;
    float moveDistance ;
    float interactDistance = 2f;

    BaseCounter selectedCounter;

    KitchenObject kitchenObject;


    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
        if (Instance != null)
        {
            Debug.LogError("There is more than 1 Player instacne");
        }
        Instance = this;
    }

    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        } 
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInterraction();

    }

    void HandleInterraction()
    {
        interactDir = gameInput.GetMovementVectorNormalized();
        interactDirV3 = new(interactDir.x, 0f, interactDir.y);
        if (interactDirV3 != Vector3.zero)
        {
            lastInteractDir = interactDirV3;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit,
            interactDistance, counterLayerMask))
        {
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }                
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    void HandleMovement()
    {
        moveDir = gameInput.GetMovementVectorNormalized();

        moveDirV3 = new Vector3(moveDir.x, 0f, moveDir.y);

        Debug.Log("V2 : " + moveDir + "V3 : " + moveDirV3);

        moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius, moveDirV3, moveDistance); // Dont hit anything ?


        if (!canMove)
        {
            Vector3 moveDirX = new(moveDirV3.x, 0, 0);
            canMove = (moveDirV3.x < -.5f || moveDirV3.x > .5f) 
                && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance); // Dont hit anything on X?

            if (canMove)
            {
                moveDirV3 = moveDirX.normalized;
                transform.position += moveDirV3 * moveDistance;

            }
            else
            {
                Vector3 moveDirZ = new(0, 0, moveDirV3.z);
                canMove = (moveDirV3.z < -.5f || moveDirV3.z > .5f) && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance); // Dont hit anything on Z?

                if (canMove)
                {
                    moveDirV3 = moveDirZ.normalized;
                    transform.position += moveDirV3 * moveDistance;
                }
                
            }
        }
        else
        {
            transform.position += moveDirV3 * moveDistance;
        }

        isWalking = moveDirV3 != Vector3.zero; // for animation

        transform.forward = Vector3.Slerp(transform.forward, moveDirV3,
            Time.deltaTime * rotateSpeed);


    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCountercharged?.Invoke(this, new OnselectedCounterchargedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
