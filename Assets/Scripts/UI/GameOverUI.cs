using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipesDeliveredText;

    GameManager instance_GameManager;

    private void Start()
    {
        instance_GameManager = GameManager.Instance;
        instance_GameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (instance_GameManager.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = 
                DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();

        }
        else
        {
            Hide();
        }
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
