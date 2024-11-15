using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene {
        StartMenu,
        Game,
        GameOverMenu
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}
