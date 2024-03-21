using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExEnemy : MonoBehaviour
{
    public ExPlayer targetPlayer;

    private int damage = 20;

    public void AttackPlayer(ExPlayer player)
    {
        player.TakeDamage(damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("АјАн");
            if (targetPlayer != null)
            {
                AttackPlayer(targetPlayer);
            }
        }
    }
}
