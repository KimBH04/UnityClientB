using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class ExJsonData : MonoBehaviour
{
    string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log(filePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData data = new PlayerData
            {
                playerName = "Player 1",
                playerLevel = 1,
                items =
                {
                    "Stone 1",
                    "Rock 1"
                }
            };
            SaveData(data);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData data = LoadData();

            Debug.Log(data);
        }
    }

    private void SaveData(PlayerData playerData)
    {
        string jsonData = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(filePath, jsonData);
    }

    PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

            return playerData;
        }
        else
        {
            return null;
        }
    }
}
