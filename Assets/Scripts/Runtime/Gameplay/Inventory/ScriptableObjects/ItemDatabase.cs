using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemData> Items;

        private Dictionary<string, ItemData> _lookup;

        void OnEnable()
        {
            _lookup = new Dictionary<string, ItemData>();
            foreach (var item in Items)
                _lookup[item.ItemId] = item;
        }

        public ItemData GetById(string id)
        {
            return _lookup.TryGetValue(id, out var item) ? item : null;
        }
    }
}
