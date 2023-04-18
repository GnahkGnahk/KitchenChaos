using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    GameManager instance_GameManager;

    Animator animator;

    int countdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        instance_GameManager = GameManager.Instance;
        instance_GameManager.OnStateChanged += GameManager_OnStateChanged;
        Hide();

    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (instance_GameManager.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(instance_GameManager.GetCountdownToStartTimer());
        countdownText.text= countdownNumber.ToString("F0");

        if (this.countdownNumber != countdownNumber)
        {
            this.countdownNumber = countdownNumber;
            animator.SetTrigger(AnimationString.TRIGGER_POPUP_NUMBER);
            SoundManager.Instance.PlayCountdownSound();
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
