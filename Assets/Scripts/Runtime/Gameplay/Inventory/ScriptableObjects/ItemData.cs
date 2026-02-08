using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemId;
        public string itemName;
        public string itemDescription;
        public Sprite icon;
        public bool stackable;
        public int maxStack = 99;

#if UNITY_EDITOR
        //TODO: This is a simple way to ensure unique IDs,
        //but it may not be ideal for all use cases.
        void OnValidate()
        {
            if (string.IsNullOrEmpty(itemId))
            {
                itemId = System.Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
