using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int directionVector = 1;
    public int speed = 7;
    private SpriteRenderer renderer;

    public void SetDirectionVector(int direction)
    {
        directionVector = direction;
    }

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += directionVector * speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -13 || pos.x > 13) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
