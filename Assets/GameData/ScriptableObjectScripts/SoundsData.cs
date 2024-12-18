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

    // The function below is the one that breaks the game when you build it. I think the internal soundAudioClips dictionary
    // that this ScriptableObject generates doesn't load properly when the game builds or something.

    // This made it so that any code that tried to play a sound through the SoundManager would break,
    // (Ex: the reason why enemies did not get knocked back is because it tried to play a sound on a previous line)

    // public AudioClip GetRandomSoundClip(Sound sound) {
    //     AudioClip[] audioClips = soundAudioClips[sound];
    //     return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    // }

    // Using this new method is a temporary solution since it is way more inefficient
    // Instead of a dictionary it just loops through the sounds array and finds the matching one

    public AudioClip GetRandomSoundClip(Sound sound) {
        foreach(Sound_AudioClipsArray sound_AudioClipArray in sound_AudioClipsList) {
            if (sound_AudioClipArray.sound == sound) {
                AudioClip[] audioClips = sound_AudioClipArray.audioClips;
                return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            }
        }
        return null;
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
