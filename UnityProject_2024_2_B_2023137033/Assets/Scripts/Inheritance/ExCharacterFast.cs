using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterFast : ExCharacter
{
    protected override void Move()
    {
        transform.Translate(Time.deltaTime * speed * 2 * Vector3.forward);
    }
}
