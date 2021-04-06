using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static float deadTime = 1f;
    private SpriteRenderer renderer;
    private Animator animator;
    private bool isDead = false;
    float deadTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deadTimer += Time.deltaTime;
            if (deadTimer >= deadTime)
            {
                Destroy(gameObject);
            }
            else if (deadTimer >= 0f)
            {
                renderer.color = new Color(1, 1, 1, 1 - deadTimer / deadTime);
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            SoundManager.instance.PlayHitSound();
            animator.SetBool("isDead", true);
            isDead = true;
        }
    }
}
