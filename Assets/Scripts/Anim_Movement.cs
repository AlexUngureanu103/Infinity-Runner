using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim_Movement : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField]
    private Rigidbody _Rb;
    [SerializeField]
    private Collider _PlayerCollider;

    [SerializeField]
    private ParticleSystem _ParticleSystem;
    private Animator animator;

    private float _CurrentForwardSpeed = 5.0f;
    private float targetForwardSpeed;
    private float minForwardSpeed = 2.5f;

    private float _HorizontalInput;

    private bool _isAlive = true;
    private bool isJumping;
    private int extraJumps;

    private bool isDisabledTrigger = false;

    private bool canRotate = true;
    private float currentYRotation = 0;

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

        animator.SetFloat("Speed", _CurrentForwardSpeed);

        if (transform.position.y < -1)
        {
            animator.SetBool("IsFalling", true);
        }

        _Rb.MovePosition(_Rb.position + forwardMove + horizontalMove);
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        playerStats = FindObjectOfType<PlayerStats>();
        extraJumps = playerStats.ExtraJumps;
    }

    void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        if (isJumping)
        {
            _PlayerCollider.contactOffset = 0.5f;
        }
        else
        {
            _PlayerCollider.contactOffset = 0.01f;
        }

        _HorizontalInput = Input.GetAxis("Horizontal");
        float _VerticalInput = Input.GetAxis("Vertical");
        if (canRotate)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentYRotation -= 90;
                canRotate = false;
                Invoke("ChangePlayerRotation", 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                canRotate = false;
                currentYRotation += 90;
                Invoke("ChangePlayerRotation", 0.1f);
            }
        }

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
            GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayJumpSound();
            _ParticleSystem.Pause();
            _Rb.isKinematic = false;
            _Rb.velocity = Vector2.up * playerStats.JumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayJumpSound();
            _ParticleSystem.Pause();
            isJumping = true;
            animator.SetBool("IsJumping", true);
            _Rb.isKinematic = false;
            _Rb.velocity = Vector2.up * playerStats.JumpForce;
            isDisabledTrigger = true;
            StartCoroutine(EnableTriggerAfterDelay(0.5f));
        }

        if (transform.position.y < -10)
        {
            _ParticleSystem.Pause();
            GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayDeathSound();
            Die();
        }
    }

    private IEnumerator EnableTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDisabledTrigger = false;
    }

    void OnCollisionExit(Collision collision)
    {
        _Rb.isKinematic = false;
    }

    private void OnTriggerExit(Collider collision)
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isDisabledTrigger)
            {
                return;
            }
            isJumping = false;
            animator.SetBool("IsJumping", false);
            Invoke("SetWalkAnimationController", 0.2f);
            _Rb.isKinematic = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("HasHitAnObstacle", true);
        _CurrentForwardSpeed = 0;
        targetForwardSpeed = 0;

        _isAlive = false;

        Invoke("Restart", 2);
    }

    void ChangePlayerRotation()
    {
        if (currentYRotation == 360 || currentYRotation == -360)
        {
            currentYRotation = 0;
        }
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        canRotate = true;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
