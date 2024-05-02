using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExEnemyManager : MonoBehaviour
{
    public List<ExEnemy> enemies = new();

    private void Start()
    {
        var sortedEnemies = enemies.OrderBy(e => Vector3.Distance(e.transform.position, transform.position));

        foreach (var enemy in sortedEnemies)
        {
            Debug.Log($"Sorted enemy : {enemy.name} {Vector3.Distance(enemy.transform.position, transform.position)}");
        }
    }
}
