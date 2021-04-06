using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int xAxis = 1;
    private float yAxis;
    private int speed = 7;

    public void SetDirectionVector
        (int directionVector)
    {
        this.xAxis = directionVector;
    }

    // Start is called before the first frame update
    void Awake()
    {
        yAxis = Random.Range(-10, 11) / 10.0f * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -10 || pos.x > 10)
        {
            remove();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            remove();
        }
    }

    void remove()
    {
        Destroy(gameObject);
    }
}