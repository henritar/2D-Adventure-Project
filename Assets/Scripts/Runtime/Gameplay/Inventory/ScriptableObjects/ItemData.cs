using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public bool stackable;
        public int maxStack = 99;
    }
}
