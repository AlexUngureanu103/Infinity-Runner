using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField]
    private Rigidbody _Rb;

    private float _CurrentForwardSpeed = 5.0f;
    private float targetForwardSpeed;
    private float minForwardSpeed = 2.5f;

    private float _HorizontalInput;


    private bool _isAlive = true;
    private bool isJumping;
    private int extraJumps;


    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }
        
        _CurrentForwardSpeed = Math.Min(Mathf.Lerp(_CurrentForwardSpeed, targetForwardSpeed, Time.deltaTime), playerStats.MaxSpeed);
        _CurrentForwardSpeed = Math.Max(_CurrentForwardSpeed, minForwardSpeed);
        Vector3 forwardMove = transform.forward * _CurrentForwardSpeed * Time.deltaTime;
        Vector3 horizontalMove = transform.right * _HorizontalInput * _CurrentForwardSpeed * Time.deltaTime;

        _Rb.MovePosition(_Rb.position + forwardMove + horizontalMove);
    }

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        extraJumps = playerStats.ExtraJumps;
    }

    void Update()
    {
        _HorizontalInput = Input.GetAxis("Horizontal");
        float _VerticalInput = Input.GetAxis("Vertical");

        // Add speed multiplier for forward and backward movement
        if (_VerticalInput > 0)
        {
            targetForwardSpeed = _CurrentForwardSpeed * playerStats.AccelerationSpeedMultiplyer;
        }
        else if (_VerticalInput < 0)
        {
            targetForwardSpeed = _CurrentForwardSpeed / playerStats.DecelerationSpeedMultiplyer;
        }
        else
        {
            targetForwardSpeed = playerStats.BaseSpeed;
        }

        if (isJumping && extraJumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _Rb.velocity = Vector2.up * playerStats.JumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            _Rb.velocity = Vector2.up * playerStats.JumpForce;
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
            extraJumps = playerStats.ExtraJumps;
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
