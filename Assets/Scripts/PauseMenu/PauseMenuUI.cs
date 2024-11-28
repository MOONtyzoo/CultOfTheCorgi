using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
   [SerializeField] private EventSystem eventSystem;
   [SerializeField] private InputReader input;

   [SerializeField] private CanvasGroup pausePanel;
   [SerializeField] private Button returnToStartScreenButton;
   [SerializeField] private Button resumeButton;

   private Button defaultSelectedButton;

   private bool isPaused = false;

   private void Awake()
   {
      defaultSelectedButton = resumeButton;
      HidePauseScreen();
      SetupButtonClickedEvents();
   }

   private void OnEnable()
   {
      input.PauseEvent += TogglePause;
   }

   private void OnDisable()
   {
      input.PauseEvent -= TogglePause;
   }
   
   private void SetupButtonClickedEvents() {
      returnToStartScreenButton.onClick.AddListener(() => ReturnToStartScreen());
      resumeButton.onClick.AddListener(() => Resume());
   }

   private void TogglePause() {
      if (isPaused) {
         Resume();
      } else {
         Pause();
      }
   }

   private void ReturnToStartScreen() {
      Time.timeScale = 1.0f;
      SceneLoader.Load(SceneLoader.Scene.StartScreen);
   }

   private void Pause() {
      Time.timeScale = 0.0f;
      isPaused = true;
      ShowPauseScreen();
      SelectDefaultButton();
   }
   
   private void Resume() {
      Time.timeScale = 1.0f;
      isPaused = false;
      HidePauseScreen();
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

   private void SelectDefaultButton() {
      eventSystem.SetSelectedGameObject(defaultSelectedButton.gameObject);
   }
}
