using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //controls
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode fire;

    //movement variables
    public float moveSpeed = 7;
    public float jumpHeight = 15f;
    private bool isGrounded;

    //fireball
    public GameObject fireBall;
    public float ballVelocity;
    public Transform shootPoint;
    private bool alreadyFired;
    public float timeBetweenFiring;

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

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorldPosition.z = 0f;
        //transform.position = mouseWorldPosition;

        //target.gameObject.GetComponent<Transform>();
        //targetFire = target.gameObject.GetComponent<Transform>();

        //firing
        if (Input.GetKeyDown(fire) && !alreadyFired)
        {
            Rigidbody2D ball = Instantiate(fireBall, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            ball.velocity = new Vector2(mouseWorldPosition.x * ballVelocity * fr * Time.deltaTime, mouseWorldPosition.y * ballVelocity * fr * Time.deltaTime);
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
