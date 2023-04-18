using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField]
    ContainerCounter containerCounter;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnGrab;

    }

    private void ContainerCounter_OnGrab(object sender, System.EventArgs e)
    {
        anim.SetTrigger(AnimationString.TRIGGER_OPEN_CLOSE);
    }
}
