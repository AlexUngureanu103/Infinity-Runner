using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _ForwardSpeedMultiplier = 1.0f;
    [SerializeField]
    private float _InitialForwardSpeed = 5.0f;
    [SerializeField]
    private float _ForwardSpeed = 5.0f;


    [SerializeField]
    private float _JumpForce = 10.0f;

    [SerializeField]
    private Rigidbody _Rb;

    [SerializeField]
    private float _HorizontalMultiplier = 2.0f;
    private float _HorizontalInput;

    private bool _isAlive = true;

    private bool isJumping;
    private int extraJumps = 1;
    private int extraJumpsValue;

    private float targetForwardSpeed;
    private float maxForwardSpeed = 10.0f;
    private float minForwardSpeed = 2.5f;

    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }
        
        _ForwardSpeed = Math.Min(Mathf.Lerp(_ForwardSpeed, targetForwardSpeed, Time.deltaTime), maxForwardSpeed);
        _ForwardSpeed = Math.Max(_ForwardSpeed, minForwardSpeed);
        Vector3 forwardMove = transform.forward * _ForwardSpeed * Time.deltaTime;
        Vector3 horizontalMove = transform.right * _HorizontalInput * _ForwardSpeed * Time.deltaTime;

        _Rb.MovePosition(_Rb.position + forwardMove + horizontalMove);
    }

    void Start()
    {
        extraJumpsValue = extraJumps;
    }

    void Update()
    {
        _HorizontalInput = Input.GetAxis("Horizontal");
        float _VerticalInput = Input.GetAxis("Vertical");

        // Add speed multiplier for forward and backward movement
        _ForwardSpeedMultiplier = 1.0f;
        if (_VerticalInput > 0)
        {
            _ForwardSpeedMultiplier = 1.5f;
            targetForwardSpeed = _ForwardSpeed * _ForwardSpeedMultiplier;
        }
        else if (_VerticalInput < 0)
        {
            _ForwardSpeedMultiplier = 0.5f;
            targetForwardSpeed = _ForwardSpeed * _ForwardSpeedMultiplier;
        }
        else
        {
            targetForwardSpeed = _InitialForwardSpeed;
        }

        if (isJumping && extraJumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _Rb.velocity = Vector2.up * _JumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            _Rb.velocity = Vector2.up * _JumpForce;
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            extraJumps = extraJumpsValue;
        }
    }

    public void Die()
    {
        _isAlive = false;

        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
