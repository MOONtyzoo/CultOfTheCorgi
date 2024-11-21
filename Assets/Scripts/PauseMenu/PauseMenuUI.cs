using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
   [SerializeField] private CanvasGroup pausePanel;
   [SerializeField] private Button returnToStartScreenButton;
   [SerializeField] private Button resumeButton;
   private void Awake()
   {
      HidePauseScreen();
      SetupButtonClickedEvents();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         ShowPauseScreen();
      }
   }
   
   private void SetupButtonClickedEvents() {
      returnToStartScreenButton.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.StartScreen));
      resumeButton.onClick.AddListener(() => HidePauseScreen());
   }

   private void ShowPauseScreen() {
      pausePanel.alpha = 1;
      pausePanel.interactable = true;
      pausePanel.blocksRaycasts = true;
   }
   
   private void HidePauseScreen() {
      pausePanel.alpha = 0;
      pausePanel.interactable = false;
      pausePanel.blocksRaycasts = false;
   }
}
