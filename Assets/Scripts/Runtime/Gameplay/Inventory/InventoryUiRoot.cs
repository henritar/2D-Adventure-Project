using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory
{
    public class InventoryUiRoot : MonoBehaviour
    {
        [SerializeField] InventoryView view;
        [SerializeField] PlayerInventory playerInventory;

        InventoryPresenter presenter;

        void Start()
        {
            presenter = new InventoryPresenter(
                playerInventory.Model,
                view
            );
        }
    }
}
