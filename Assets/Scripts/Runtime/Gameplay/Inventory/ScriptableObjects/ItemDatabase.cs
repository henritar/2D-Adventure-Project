using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemData> items;

        private Dictionary<string, ItemData> lookup;

        void OnEnable()
        {
            lookup = new Dictionary<string, ItemData>();
            foreach (var item in items)
                lookup[item.itemId] = item;
        }

        public ItemData GetById(string id)
        {
            return lookup.TryGetValue(id, out var item) ? item : null;
        }
    }
}
