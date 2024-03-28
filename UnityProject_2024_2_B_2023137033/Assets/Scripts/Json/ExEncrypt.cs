using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Newtonsoft.Json;

public class ExEncrypt : MonoBehaviour
{
    string filePath;
    readonly string key = "ThisIsASecretKey";

    private void Start()
    {
        filePath = Application.persistentDataPath + "/EncryptPlayerData";
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

        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(jsonData);
        byte[] encryptedBytes = Encrypt(bytesToEncrypt);
        string encryptedData = Convert.ToBase64String(encryptedBytes);

        File.WriteAllText(filePath, encryptedData);
    }

    private PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedBytes = Decrypt(encryptedBytes);
            string jsonData = Encoding.UTF8.GetString(decryptedBytes);

            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return playerData;
        }
        else
        {
            return null;
        }
    }

    private byte[] Encrypt(byte[] plainBytes)
    {
        using Aes aseAlg = Aes.Create();

        aseAlg.Key = Encoding.UTF8.GetBytes(key);
        aseAlg.IV = new byte[16];

        ICryptoTransform encryptor = aseAlg.CreateEncryptor(aseAlg.Key, aseAlg.IV);

        using MemoryStream mStream = new MemoryStream();
        using CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);

        cStream.Write(plainBytes, 0, plainBytes.Length);
        cStream.FlushFinalBlock();

        return mStream.ToArray();
    }

    private byte[] Decrypt(byte[] encryptedBytes)
    {
        using Aes aesAlg = Aes.Create();

        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        aesAlg.IV = new byte[16];

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream mStream = new MemoryStream(encryptedBytes);
        using CryptoStream cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read);

        byte[] decryptedBytes = new byte[encryptedBytes.Length];
        int decryptedByteCount = cStream.Read(decryptedBytes, 0, decryptedBytes.Length);

        return decryptedBytes.Take(decryptedByteCount).ToArray();
    }
}
