using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject hasProgressGameObject;
    IHasProgesss hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgesss>();
        if (hasProgress == null)
        {
            Debug.LogError("GameObject" + hasProgressGameObject + " doesnt have IHasProgesss");
            return;
        }
        hasProgress.OnprogressChanged += HasProgress_OnprogressChanged;
        barImage.fillAmount = 0f;

        Hide();
    }

    private void HasProgress_OnprogressChanged(object sender, IHasProgesss.OnprogressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNomalized;
        if (barImage.fillAmount == 0 || barImage.fillAmount == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
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
