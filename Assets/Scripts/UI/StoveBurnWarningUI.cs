using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;
        Hide();
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProgesss.OnprogressChangedEventArgs e)
    {
        float burnShowProgressAmount = .2f;
        bool show = (stoveCounter.IsFried())
            && e.progressNomalized >= burnShowProgressAmount;

        if (show)
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
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
