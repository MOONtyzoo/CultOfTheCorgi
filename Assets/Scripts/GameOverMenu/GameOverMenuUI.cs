using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] private Button startMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Text scoreText;
    private void Awake()
    {
        SetupDefaultButtonEvent();
        SetupButtonClickedEvents();

        if (ScoreManager.Instance.GetScore() < 10)
            scoreText.text = "0" + ScoreManager.Instance.GetScore();
        else
            scoreText.text = "" + ScoreManager.Instance.GetScore();
        
        scoreText.text = "- <color=maroon><b>" + scoreText.text + "</b></color> -";
    }

    private void SetupButtonClickedEvents()
    {
        startMenu.onClick.AddListener(() =>SceneLoader.Load(SceneLoader.Scene.StartScreen));
    }

    private void SetupDefaultButtonEvent()
    {
        Button defaultButton = startMenu;
        eventSystem.SetSelectedGameObject(defaultButton.gameObject);
    }
}
