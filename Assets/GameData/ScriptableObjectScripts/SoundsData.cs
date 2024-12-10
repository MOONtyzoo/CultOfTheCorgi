using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="GameSoundsData", menuName="GameData/GameSounds")]
public class GameSoundsData : ScriptableObject
{
    public enum Sound {
        Swipe,
        Impact,
        PlayerHurt,
    }

    [SerializeField] private List<Sound_AudioClipsArray> sound_AudioClipsList;
    [Serializable] 
    private struct Sound_AudioClipsArray {
        public Sound sound;
        public AudioClip[] audioClips;
    }

    public Dictionary<Sound, AudioClip[]> soundAudioClips = new Dictionary<Sound, AudioClip[]>();

    public AudioClip GetRandomSoundClip(Sound sound) {
        AudioClip[] audioClips = soundAudioClips[sound];
        return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    }

    private void OnValidate() {
        soundAudioClips.Clear();
        foreach(Sound_AudioClipsArray sound_AudioClipArray in sound_AudioClipsList) {
            if (soundAudioClips.ContainsKey(sound_AudioClipArray.sound)) {
                Debug.LogError($"There is already a list entry for sound ({sound_AudioClipArray.sound})!!!");
                break;
            } else {
                soundAudioClips.Add(sound_AudioClipArray.sound, sound_AudioClipArray.audioClips);
            }
        }
    }

    private void Debug_LogDictionary() {
        foreach(KeyValuePair<Sound, AudioClip[]> entry in soundAudioClips) {
            Debug.Log($"Sound: {entry.Key.ToString()} --- AudioClipArray[{entry.Value.Length}]");
        }
    }
}
