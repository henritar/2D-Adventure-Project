using Assets.Scripts.Runtime.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.Sound.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Audio/Sound")]
    public class SoundData : ScriptableObject
    {
        public string Id;    
        public SoundsEnum SoundEnum;
        public AudioClip Clip;
        [Range(0f, 1f)] 
        public float Volume = 1f;
        [Range(0.5f, 2f)] 
        public float Pitch = 1f;
    }

}
