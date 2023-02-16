using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //controls
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.W;
    [SerializeField] private KeyCode fire = KeyCode.Mouse0;

    //movement variables
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpHeight = 15f;
    private bool isGrounded;

    //fireball
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float ballVelocity = 3;
    [SerializeField] private float timeBetweenFiring = 1;
    private bool alreadyFired;

    //audio
    [SerializeField] AudioSource soundManager;
    [SerializeField] AudioClip fwoosh;

    //declarations
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject target;
    private int fr = 60;

    private void Start()
    {
        Application.targetFrameRate = fr;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //left-right
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            rb.velocity = new Vector2(-moveSpeed * fr * Time.deltaTime, rb.velocity.y * fr * Time.deltaTime);
        }
        else if (Input.GetKey(right) && !Input.GetKey(left))
        {
            rb.velocity = new Vector2(moveSpeed * fr * Time.deltaTime, rb.velocity.y * fr * Time.deltaTime);
        }

        //jumping
        if (Input.GetKey(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x * fr * Time.deltaTime, jumpHeight * fr * Time.deltaTime);
        }
        
        //firing
        Vector2 targetPosition = target.transform.localPosition;
    
        if (Input.GetKeyDown(fire) && !alreadyFired)
        {
            Rigidbody2D ball = Instantiate(fireBall, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            ball.velocity = new Vector2(targetPosition.x * ballVelocity * fr * Time.deltaTime, targetPosition.y * ballVelocity * fr * Time.deltaTime);

            alreadyFired = true;
            Invoke(nameof(ResetFire), timeBetweenFiring);

            soundManager.PlayOneShot(fwoosh);
        }
    }
    private void ResetFire()
    {
        alreadyFired = false;
    }
}
