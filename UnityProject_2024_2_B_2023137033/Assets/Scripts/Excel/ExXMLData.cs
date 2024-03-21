using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerData
{
    public string playerName;
    public int playerLevel;
    public List<string> items = new List<string>();

    public override string ToString()
    {
        return $"{playerName} : Lv.{playerLevel}, {{{string.Join(", ", items)}}}";
    }
}

public class ExXMLData : MonoBehaviour
{
    string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/PlayerData.xml";
        Debug.Log(filePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData data = new PlayerData()
            {
                playerName = "name",
                playerLevel = 1,
                items = { "µπ1", "πŸ¿ß1" }
            };

            SaveData(data);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData data = LoadData();

            Debug.Log(data);
        }
    }

    private void SaveData(PlayerData data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        using FileStream stream = new FileStream(filePath, FileMode.Create);

        serializer.Serialize(stream, data);
    }

    PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            using FileStream stream = new FileStream(filePath, FileMode.Open);
            return (PlayerData)serializer.Deserialize(stream);
        }
        else
        {
            return null;
        }
    }
}
