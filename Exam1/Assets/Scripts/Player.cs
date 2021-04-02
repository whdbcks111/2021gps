using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private int hp = 3;
    private SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            hp -= 1;
            Debug.Log(hp);
        }
    }

    void Update()
    {
        if (hp > 0) { 
            float posX = Input.GetAxis("Horizontal");
            float posY = Input.GetAxis("Vertical");
            Vector3 pos = gameObject.transform.position;
            pos.x += posX * Time.deltaTime * speed;
            pos.y += posY * Time.deltaTime * speed;

            if (posX != 0) renderer.flipX = posX < 0;

            if (pos.x > 11.2f) pos.x = 11.2f;
            else if (pos.x < -11.2f) pos.x = -11.2f;

            if (pos.y > 5.2f) pos.y = 5.2f;
            else if (pos.y < -5.2f) pos.y = -5.2f;

            gameObject.transform.position = pos;
        }
        
    }

    public int GetHp()
    {
        return hp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }
}
