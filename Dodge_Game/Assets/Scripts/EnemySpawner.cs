using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;
    public GameObject enemyGroup;
    private bool isPlaying = true;
    private UI_Time timer;

    public void OffSpawner()
    {
        isPlaying = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        timer = FindObjectOfType<UI_Time>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            float spawnPer = Random.Range(0, 1001) / 10f;
            if (spawnPer < 1 + Mathf.Min(timer.getPlayingTime() * 0.02f, 4f))
            {
                Enemy e = Instantiate(enemy);

                float posY = Random.Range(-8f, 8f);
                int isLeftInstatiate = Random.Range(0, 2);
                
                SpriteRenderer renderer = e.GetComponent<SpriteRenderer>();

                if (isLeftInstatiate == 0)
                {
                    e.transform.position =
                        new Vector3(-8.7f, posY, -1);
                    e.SetDirectionVector(1);
                }
                else
                {
                    e.transform.position = new Vector3(8.7f, posY, -1);
                    renderer.flipX = true;
                    e.SetDirectionVector(-1);
                }

                renderer.color = new Color(Random.value, Random.value, Random.value);
                e.transform.localScale = e.transform.localScale * (1 + Random.value * 0.5f);
                e.transform.parent = enemyGroup.transform;
            }
        }
    }
}
