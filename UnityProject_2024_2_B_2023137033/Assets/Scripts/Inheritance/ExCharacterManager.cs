using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterManager : MonoBehaviour
{
    public List<ExCharacter> list = new List<ExCharacter>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var character in list)
            {
                character.DestrotyCharacter();
            }
        }
    }
}
