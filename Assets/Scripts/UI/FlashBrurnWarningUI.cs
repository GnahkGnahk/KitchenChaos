using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBrurnWarningUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;

        animator.SetBool(AnimationString.BOOL_IS_FLASHING, false);
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProgesss.OnprogressChangedEventArgs e)
    {
        float burnShowProgressAmount = .2f;
        bool show = stoveCounter.IsFried()
            && e.progressNomalized >= burnShowProgressAmount;

        animator.SetBool(AnimationString.BOOL_IS_FLASHING, show);
    }

}
