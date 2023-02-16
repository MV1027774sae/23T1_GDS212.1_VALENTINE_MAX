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

    //declarations
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private int fr = 60;


    private Vector2 targetFire;


    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject target;

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
        else
        {
            //rb.velocity = new Vector2(0, rb.velocity.y * fr * Time.deltaTime);
        }

        //jumping
        if (Input.GetKey(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x * fr * Time.deltaTime, jumpHeight * fr * Time.deltaTime);
        }

        //Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mouseScreenPosition = (Input.mousePosition);
        Vector2 targetPosition = target.transform.localPosition;
        //mouseWorldPosition.z = 0f;
        //transform.position = mouseWorldPosition;

        //target.gameObject.GetComponent<Transform>();
        //targetFire = target.gameObject.GetComponent<Transform>();

        //firing
        if (Input.GetKeyDown(fire) && !alreadyFired)
        {
            Rigidbody2D ball = Instantiate(fireBall, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            ball.velocity = new Vector2(targetPosition.x * ballVelocity * fr * Time.deltaTime, targetPosition.y * ballVelocity * fr * Time.deltaTime);
                //(Vector2.velocit = mouseWorldPosition * 1, ForceMode2D.Force);

            alreadyFired = true;
            Invoke(nameof(ResetFire), timeBetweenFiring);

            //Destroy(fireBall.gameObject, 2f);
        }
    }
    private void ResetFire()
    {
        alreadyFired = false;
    }
}
