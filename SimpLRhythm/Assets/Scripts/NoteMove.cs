using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteMove : MonoBehaviour
{
    public bool isInHitter = false;
    public float time;
    public bool check = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.transform.name);
        if (other.gameObject.tag.Equals("Hitter"))
        {
            isInHitter = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Hitter"))
        {
            isInHitter = false;
            if (!check)
            {
                check = true;
            }
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
        if (!check)
        {
            check = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(0, GameManager.screenPixelHeight * Mathf.Clamp(Random.value, 0.3f, 0.7f)));
        pos.z = 0;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * GameManager.noteDistance / GameManager.noteMoveTime * Time.deltaTime;
        if (transform.position.x >
            Camera.main.ScreenToWorldPoint(new Vector3(GameManager.screenPixelWidth, 0, 0)).x + 1)
        {
            Remove();
            GameManager.instance.ShowMiss();
        }
    }
}
