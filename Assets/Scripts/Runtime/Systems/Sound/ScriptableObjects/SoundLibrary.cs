using Assets.Scripts.Runtime.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.Sound.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Audio/Sound Library")]
    public class SoundLibrary : ScriptableObject
    {
        public List<SoundData> Sounds;

        private Dictionary<SoundsEnum, SoundData> _lookup;

        void OnEnable()
        {
            _lookup = new Dictionary<SoundsEnum, SoundData>();
            foreach (var sound in Sounds)
                _lookup[sound.SoundEnum] = sound;
        }

        public SoundData Get(SoundsEnum soundsEnum)
        {
            return _lookup.TryGetValue(soundsEnum, out var sound) ? sound : null;
        }
    }

}
