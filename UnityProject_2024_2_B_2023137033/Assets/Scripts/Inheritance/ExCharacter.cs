using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacter : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.forward);
    }

    public void DestrotyCharacter()
    {
        Destroy(gameObject);
    }
}
