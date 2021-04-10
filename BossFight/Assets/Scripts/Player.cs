using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerMoveSpeed = 5f;
    public float gravity = -9.8f;
    private float velocity;
    public float jumpPower = 5f;

    private bool isJump = false;

    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            velocity = 0;
            isJump = false;
        }
        
        if (Input.GetButton("Jump") && !isJump)
        {
            isJump = true;
            velocity = jumpPower;
        }
        

        velocity += gravity * Time.deltaTime;
        dir.y = velocity;
        //dir = Camera.main.transform.TransformDirection(dir);

        cc.Move(dir * playerMoveSpeed * Time.deltaTime);
    }
}
