using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    AudioSource audioSource;

    bool playerWarnningSound;
    float warningSoundTimerMax = .2f;
    float warningSoundTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;

        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;

    }

    private void StoveCounter_OnprogressChanged(object sender,
        IHasProgesss.OnprogressChangedEventArgs e )
    {

        float burnShowProgressAmount = .2f;
        playerWarnningSound = stoveCounter.IsFried()
            && e.progressNomalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, 
        StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Fring ||
            e.state == StoveCounter.State.Fried ||
            e.state == StoveCounter.State.Burned)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

    }

    private void Update()
    {
        if (playerWarnningSound)
        {
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer <= 0f)
            {
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
        
    }
}
