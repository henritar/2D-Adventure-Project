using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using Assets.Scripts.Runtime.Manager;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    public class PlayerInventory : MonoBehaviour
    {

        [SerializeField] 
        ItemDatabase itemDatabase;
        public InventoryModel Model { get; private set; }

        void Awake()
        {
            Model = new InventoryModel(20);
        }

        void Start()
        {
            var save = SaveManager.LoadInventory();
            if (save != null)
                Model.LoadFromSave(save, itemDatabase);
        }

        void OnApplicationQuit()
        {
            SaveManager.SaveInventory(Model);
        }
    }
}
