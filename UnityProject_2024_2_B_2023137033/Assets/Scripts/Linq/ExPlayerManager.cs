using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExPlayerManager : MonoBehaviour
{
    public List<PlayerData> playerDatas = new();

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            PlayerData data = new()
            {
                playerName = $"player {i}",
                playerLevel = Random.Range(0, 20)
            };

            playerDatas.Add(data);
        }

        var highLevelPlayer = playerDatas.Where(x => x.playerLevel >= 10);

        foreach (var player in highLevelPlayer)
        {
            Debug.Log($"High level player : {player}");
        }
    }
}
