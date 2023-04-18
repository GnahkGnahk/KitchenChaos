using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour {
    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] Button soundEffectsBtn, musicBGBtn, closeBtn;
    TextMeshProUGUI soundEffectsText, musicBGText;


    [SerializeField]
    TextMeshProUGUI moveUpTxt, moveDownTxt, moveLeftTxt, moveRightTxt,
         interactTxt, interactAltTxt, pauseTxt;
    [SerializeField]
    Button moveUpBtn, moveDownBtn, moveLeftBtn, moveRightBtn,
        interactBtn, interactAltBtn, pauseBtn;

    [SerializeField] Transform pressToRebindKeyTransform;

    private void Awake()
    {
        Instance = this;

        soundEffectsBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicBGBtn.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        moveUpBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAltBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlt);
        });
        pauseBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        UpdateVisual();

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        LoadMusicAndSoundEffect();
        LoadBindingText();
    }

    void LoadMusicAndSoundEffect()
    {
        soundEffectsText = soundEffectsBtn.GetComponentInChildren<TextMeshProUGUI>();
        soundEffectsText.text = "SOUND EFFECTS : " + (SoundManager.Instance.ReturnVolume() * 10f).ToString("F0");

        musicBGText = musicBGBtn.GetComponentInChildren<TextMeshProUGUI>();
        musicBGText.text = "MUSIC BACKGROUND : " + (MusicManager.Instance.ReturnVolume() * 10f).ToString("F0");

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
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    public void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            LoadBindingText();
        });
    }
}
