using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    Player player;

    float footStepTimer, footStepTimerMax = 0.1f;

    [SerializeField, Range(0f,1f)] float volume;

    private void Awake()
    {
        player = GetComponent<Player>();
    }


    private void Update()
    {
        footStepTimer -= Time.deltaTime;

        if (footStepTimer < 0f)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
            
        }
    }
}
