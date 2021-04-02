using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;
    public GameObject enemyGroup;
    private bool isPlaying = true;

    public void SpawnerOff()
    {
        isPlaying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            float spawnPer = Random.Range(0, 1001) / 10.0f;
            if (spawnPer < 2.0)
            {
                bool isLeft = Random.Range(0, 2) == 0;
                Enemy e = Instantiate(enemy);
                float posY = Random.Range(-5.2f, 5.2f);
                e.transform.position = new Vector3(isLeft ? -9 : 9, posY, 0);
                e.GetComponent<SpriteRenderer>().flipX = !isLeft;
                e.SetDirectionVector(isLeft ? 1 : -1);
            }
        }
    }
}
