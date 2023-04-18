using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI moveUpTxt, moveDownTxt, moveLeftTxt, moveRightTxt, interactTxt, 
        interactAltTxt, pauseTxt, moveGPTxt, interactGPTxt, interactAltGPTxt, 
        pauseGPTxt;

    private void Start()
    {
        GameInput.Instance.OnbindingRebind += GameInput_OnbindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnbindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
        Show();
    }

    void UpdateVisual()
    {
        LoadBindingText();
    }

    void LoadBindingText()
    {
        moveUpTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        interactGPTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltGPTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseGPTxt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
