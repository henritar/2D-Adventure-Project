using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Systems.Save.DTO;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager
{
    public static class SaveManager
    {
        private static string InventoryPath =>
            Path.Combine(Application.persistentDataPath, "inventory.json");

        public static void SaveInventory(InventoryModel inventory)
        {
            var data = inventory.GetSaveData();
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(InventoryPath, json);
        }

        public static InventorySaveData LoadInventory()
        {
            if (!File.Exists(InventoryPath))
                return null;

            var json = File.ReadAllText(InventoryPath);
            return JsonUtility.FromJson<InventorySaveData>(json);
        }
    }
}
