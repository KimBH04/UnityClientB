using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerPrefabsData : MonoBehaviour
{
    private const string SCORE = "Score";

    public int scorePoint;

    private void SaveData(int score)
    {
        PlayerPrefs.SetInt(SCORE, score);
        PlayerPrefs.Save();
    }

    private int LoadData()
    {
        return PlayerPrefs.GetInt(SCORE, -1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData(scorePoint);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log($"{SCORE} : {LoadData()}");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey(SCORE);
        }
    }
}
