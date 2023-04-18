using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioClipRefsSO AudioClipRefsSO;

    float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PlayerPrefsString.SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        Player.Instance.OnPickSomething += Player_OnPickSomething;

        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;

        TrashCounter.OnAnyObjTrashed += TrashCounter_OnAnyObjTrashed;
    }

    private void TrashCounter_OnAnyObjTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(AudioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(AudioClipRefsSO.objDrop, baseCounter.transform.position);
    }

    private void Player_OnPickSomething(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(AudioClipRefsSO.objPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter) sender;
        PlaySound(AudioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(AudioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(AudioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(AudioClipRefsSO.footStep, position, volumeMultiplier * volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(AudioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 pos)
    {
        PlaySound(AudioClipRefsSO.warning, pos);
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PlayerPrefsString.SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float ReturnVolume()
    {
        return volume;
    }
}
