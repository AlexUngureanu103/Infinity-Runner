using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 5.0f;

    [SerializeField]
    private float _JumpForce = 5.0f;

    [SerializeField]
    private Rigidbody _Rb;

    [SerializeField]
    private float _HorizontalMultiplier = 2.0f;
    private float _HorizontalInput;

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * _Speed * Time.deltaTime;
        Vector3 horizontalMove = transform.right * _HorizontalInput * _Speed * Time.deltaTime;

        _Rb.MovePosition(_Rb.position + forwardMove + horizontalMove);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _HorizontalInput = Input.GetAxis("Horizontal");
    }
}
