using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [Header("Main Submenu")][Space]
    [SerializeField] private CanvasGroup mainPanel;

    [SerializeField] private Button startButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button exitButton;

    [Header("Controls Submenu")][Space]
    [SerializeField] private CanvasGroup controlsPanel;

    [SerializeField] private Button controlsBackButton;

    enum Submenu {
        Main,
        Controls,
    }
    private Submenu currentSubmenu;
    Dictionary<Submenu, CanvasGroup> submenuPanels = new Dictionary<Submenu, CanvasGroup>();

    void Awake() {
        submenuPanels.Add(Submenu.Main, mainPanel);
        submenuPanels.Add(Submenu.Controls, controlsPanel);
        Time.timeScale = 1.0f;

        SetupButtonClickedEvents();
        SwitchToSubmenu(Submenu.Main);
    }

    private void SetupButtonClickedEvents() {
        startButton.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.Game));
        controlsButton.onClick.AddListener(() => SwitchToSubmenu(Submenu.Controls));
        exitButton.onClick.AddListener(() => Application.Quit());

        controlsBackButton.onClick.AddListener(() => SwitchToSubmenu(Submenu.Main));
    }

    private void SwitchToSubmenu(Submenu newSubmenu) {
        HideSubmenu(currentSubmenu);
        ShowSubmenu(newSubmenu);
        currentSubmenu = newSubmenu;
    }

    private void ShowSubmenu(Submenu submenu) {
        CanvasGroup submenuPanel = submenuPanels[submenu];
        submenuPanel.alpha = 1;
        submenuPanel.interactable = true;
        submenuPanel.blocksRaycasts = true;
    }

    private void HideSubmenu(Submenu submenu) {
        CanvasGroup submenuPanel = submenuPanels[submenu];
        submenuPanel.alpha = 0;
        submenuPanel.interactable = false;
        submenuPanel.blocksRaycasts = false;
    }
}
