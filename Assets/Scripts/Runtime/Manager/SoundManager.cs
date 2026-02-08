using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Systems.Sound.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Library")]
        [SerializeField] private SoundLibrary library;

        [Header("Sources")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        [Header("Volumes")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 1f;

        public static SoundManager Instance { get; private set; }

        void Awake()
        {
            //TODO: Change to use a more robust singleton pattern,
            //especially if we want to add features like multiple sound managers in the future
            //(e.g., for different scenes or areas).
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            ApplyVolumes();
        }

        public void PlaySFX(SoundsEnum soundsEnum)
        {
            var sound = library.Get(soundsEnum);
            if (sound == null) return;

            sfxSource.pitch = sound.Pitch;
            sfxSource.PlayOneShot(sound.Clip, sound.Volume * masterVolume * sfxVolume);
        }

        public void PlayMusic(SoundsEnum soundsEnum, bool loop = true)
        {
            var sound = library.Get(soundsEnum);
            if (sound == null) return;

            musicSource.clip = sound.Clip;
            musicSource.volume = sound.Volume * masterVolume * musicVolume;
            musicSource.loop = loop;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void ApplyVolumes()
        {
            sfxSource.volume = masterVolume * sfxVolume;
            musicSource.volume = masterVolume * musicVolume;
        }
    }

}
