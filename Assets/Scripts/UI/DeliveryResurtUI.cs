using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResurtUI : MonoBehaviour
{
    [SerializeField] Image bgImage, iconImage;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Color successColor, failedColor;
    [SerializeField] Sprite successSprite, failedSprite;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += Delivery_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += Delivery_OnDeliveryFailed;

        Hide();
    }

    private void Delivery_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        bgImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "Delivery\nfailed";
        Show();
        animator.SetTrigger(AnimationString.TRIGGER_POPUP_RESULT);
    }

    private void Delivery_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        bgImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nsuccess";
        Show();
        animator.SetTrigger(AnimationString.TRIGGER_POPUP_RESULT);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
