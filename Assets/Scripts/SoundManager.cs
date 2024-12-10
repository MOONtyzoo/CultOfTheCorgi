using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] public GameSoundsData gameSounds;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("There can not be more than one SoundManager instance!");
            Destroy(gameObject);
        }
    }

    public void PlaySound(GameSoundsData.Sound sound, Vector3 position, float volume = 1f) {
        AudioClip audioClip = gameSounds.GetRandomSoundClip(sound);
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
