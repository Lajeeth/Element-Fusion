using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Epitybe.VRInventory
{
    public class InventoryManager : MonoBehaviour
    {
        public List<InventoryData> initialList = new List<InventoryData>();
        InventorySlot[] slots;
        string path;

        void Awake()
        {
            slots = GetComponentsInChildren<InventorySlot>();
            PopulateDataInSlot(initialList);
            path = Application.persistentDataPath + "/inventoryData.dat";
        }

        public int GetItemQuantityTotal(string name)
        {
            int total = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (null == slots[i].inventoryData.item)
                {
                    continue;
                }

                if (slots[i].inventoryData.item.displayPrefab.name == name)
                {
                    total += slots[i].inventoryData.count;
                }
            }
            return total;
        }

        public List<InventoryData> GetCurrentSlotsData()
        {
            List<InventoryData> inventoryData = new List<InventoryData>();
            for (int i = 0; i < slots.Length; i++)
            {
                inventoryData.Add(slots[i].inventoryData);
            }
            return inventoryData;
        }

        public void SaveData()
        {
            InventoryDataStore dataStore = new InventoryDataStore();
            dataStore.inventoryData = GetCurrentSlotsData();
            var json = JsonUtility.ToJson(dataStore);

            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, json);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public void LoadData()
        {
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                InventoryDataStore dataStore = new InventoryDataStore();

                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(fs), dataStore);
                    PopulateDataInSlot(dataStore.inventoryData);
                }
                catch (SerializationException e)
                {
                    Debug.LogError("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        void PopulateDataInSlot(List<InventoryData> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                if (i >= slots.Length)
                {
                    return;
                }
                slots[i].inventoryData = dataList[i];
                slots[i].UpdateSlot();
            }
        }
    }
}