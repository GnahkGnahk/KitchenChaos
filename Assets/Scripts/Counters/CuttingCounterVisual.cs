using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField]
    CuttingCounter cuttingCounter;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;

    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        anim.SetTrigger(AnimationString.TRIGGER_CUTTING);
    }
}
