using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Systems.Save.DTO
{
    [Serializable]
    public class InventorySaveData
    {
        public List<InventorySlotSaveData> slots = new();
    }
}
