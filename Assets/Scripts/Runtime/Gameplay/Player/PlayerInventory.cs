using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public InventoryModel Model { get; private set; }

        void Awake()
        {
            Model = new InventoryModel(20);
        }
    }
}
