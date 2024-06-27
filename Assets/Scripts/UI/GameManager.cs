using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public bool isTwoPlayer;
    public AudioInfo[] sounds;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        foreach (AudioInfo soundInfo in sounds)
        {
            soundInfo.audioSource = gameObject.AddComponent<AudioSource>();
            soundInfo.audioSource.clip = soundInfo.audioClip;
            soundInfo.audioSource.volume = soundInfo.volume;
            soundInfo.audioSource.loop = soundInfo.loop;
        }


    }

    private void Start()
    {
        //Instance.PlayAudio(AudioType.MAIN_MENU);
    }


    public void PlayAudio(AudioType audioType)
    {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Play();
    }

    public void StopAudio(AudioType audioType)
    {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Stop();
    }

    [Serializable]
    public class AudioInfo
    {
        public AudioType audioType;
        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float volume;
        public bool loop;

        [HideInInspector]
        public AudioSource audioSource;
    }
    public enum AudioType
    {
        MAIN_MENU,
        LEVEL,
        BUTTON_CLICK,
        FOOD_PICKUP,
        POWER_PICKUP,
    }
}
