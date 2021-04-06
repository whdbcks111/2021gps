using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int hp = 3;
    private SpriteRenderer renderer;
    
    //Getter
    public int GetHP() {
        return hp;
    }
    //Setter
    public void SetHP(int hp)
    {
        this.hp = hp;
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            float posX = 0, posY = 0;
            //if (Input.GetKey(KeyCode.A))
            //{
            //    posX -= 1;
            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    posX += 1;
            //}
            //if (Input.GetKey(KeyCode.W))
            //{
            //    posY += 1;
            //}
            //if (Input.GetKey(KeyCode.S))
            //{
            //    posY -= 1;
            //}

            posX = Input.GetAxis("Horizontal");
            posY = Input.GetAxis("Vertical");

            if (posX != 0)
            {
                renderer.flipX = posX < 0;
            }

            Vector3 pos = gameObject.transform.position;
            pos.x += posX * Time.deltaTime * 10;
            pos.y += posY * Time.deltaTime * 15;

            if (pos.x < -8.3f)
            {
                pos.x = -8.3f;
            }
            else if (pos.x > 8.3f)
            {
                pos.x = 8.3f;
            }

            if (pos.y < -4.7f)
            {
                pos.y = -4.7f;
            }
            else if (pos.y > 4.7f)
            {
                pos.y = 4.7f;
            }

            gameObject.transform.position = pos;
        }
    }

    void damage()
    {
        hp--;
        renderer.color = new Color(1f, 0.6f, 0.6f);
        Invoke("healImage", 0.4f);
    }

    void healImage()
    {
        renderer.color = new Color(1f, 1f, 1f);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            damage();
        }
    }
    
}