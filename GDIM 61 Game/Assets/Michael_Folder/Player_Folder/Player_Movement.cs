using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody rb;

    private float x;

    public void SetVelocityToZero()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * movementSpeed, 0);
    }
}
