using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string ItemId;
        public string ItemName;
        public string ItemDescription;
        public Sprite Icon;
        public bool Stackable;
        public int MaxStack = 99;

#if UNITY_EDITOR
        //TODO: This is a simple way to ensure unique IDs,
        //but it may not be ideal for all use cases.
        void OnValidate()
        {
            if (string.IsNullOrEmpty(ItemId))
            {
                ItemId = System.Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
