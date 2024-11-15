using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderButton : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene sceneToLoad;
    private Button button;

    void Awake() {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    public void LoadScene() {
        SceneLoader.Load(sceneToLoad);
    }
}
