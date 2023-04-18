using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button quitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GamePlayScene);
        });

        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}
