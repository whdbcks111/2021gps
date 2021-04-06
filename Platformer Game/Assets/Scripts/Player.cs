using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;

    private Rigidbody2D rigid;
    private bool isGround = true;

    public Transform groundChecker;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("isAttack"))
        {
            animator.SetBool("isAttack", true);
            SoundManager.instance.PlayAttackSound();
            Invoke("StopAttack", 0.3f);
        }

        if (animator.GetBool("isAttack"))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position 
                                                           + (transform.eulerAngles.y > 0 ? 
                                                               Vector3.left : Vector3.right) * 0.9f, 0.9f, 
                LayerMask.GetMask("Enemy"));
            foreach(Collider2D col in cols)
            {
                if (col != null)
                {
                    col.GetComponent<Enemy>().Die();
                }
            }
        }
    }

    void StopAttack()
    {
        animator.SetBool("isAttack", false);
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundChecker.position, groundRadius, groundLayer);
        Move();
        Jump();
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && isGround)
        {
            Vector3 vel = rigid.velocity;
            vel.y = jumpPower;
            rigid.velocity = vel;
            isGround = false;
        }
    }

    void Move()
    {
        float posX = Input.GetAxis("Horizontal");
        if (posX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (posX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        transform.Translate(Mathf.Abs(posX) * Vector3.right * moveSpeed * Time.deltaTime);
    }
}
