using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_T : MonoBehaviour
{
    GameInput_T gameInput_T;

    Vector2 moveDir;
    Vector3 moveDirV3;

    float moveDistance;
    [SerializeField]
    float moveSpeed, rotateSpeed;

    [SerializeField] GameObject leftArm, rightArm;
    bool leftArmIsActive, rightArmIsActive;


    private void Awake()
    {
        gameInput_T = GetComponent<GameInput_T>();
        
    }

    void Start()
    {
        leftArmIsActive = true;
        rightArmIsActive = true;

        gameInput_T.OnInteractAction += GameInput_T_OnInteractAction;
        gameInput_T.OnInteractAlternateAction += GameInput_T_OnInteractAlternateAction;

    }

    private void GameInput_T_OnInteractAlternateAction(object sender, EventArgs e)
    {
        Debug.Log("GameInput_T_OnInteractAlternateAction");
        if (leftArmIsActive)
        {
            leftArm.SetActive(false);
            leftArmIsActive = !leftArmIsActive;
        }
        else
        {
            leftArm.SetActive(true);
            leftArmIsActive = !leftArmIsActive;
        }
    }

    private void GameInput_T_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log("GameInput_T_OnInteractAction");
        if (rightArmIsActive)
        {
            rightArm.SetActive(false);
            rightArmIsActive = !rightArmIsActive;
        }
        else
        {
            rightArm.SetActive(true);
            rightArmIsActive = !rightArmIsActive;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInterraction();

    }

    void HandleInterraction()
    {
        
    }

    void HandleMovement()
    {
        moveDir = gameInput_T.GetMovementVectorNormalized();

        moveDirV3 = new Vector3(moveDir.x, 0f, moveDir.y);

        moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDirV3 * moveDistance;

        transform.forward = Vector3.Slerp(transform.forward, moveDirV3,
        Time.deltaTime* rotateSpeed);
    }
}
