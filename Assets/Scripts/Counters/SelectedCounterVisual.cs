using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;


    private void Start()
    {
        Player.Instance.OnSelectedCountercharged += Player_OnselectedCountercharged;
    }

    private void Player_OnselectedCountercharged(object sender, Player.OnselectedCounterchargedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
        
    }

    private void Hide()
    {
        foreach (var item in visualGameObjectArray)
        {
            item.SetActive(false);
        }
        
    }

    private void Show()
    {
        foreach (var item in visualGameObjectArray)
        {
            item.SetActive(true);
        }

    }
}
