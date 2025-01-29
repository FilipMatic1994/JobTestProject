using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform orientation;
    [SerializeField] private ParticleSystem runDust;
    [SerializeField] private Animator anim;

    private float horisontalInput;
    private float verticalInput;
    private float moveSpeed;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private ParticleSystem jumpDust;
    private bool readyToJump;

    [Header("Pause Menu")]
    [SerializeField] private GameObject UIPauseMenu;
    private bool isGamePaused;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject bulletSpawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = baseMoveSpeed;
        rb.freezeRotation = true;
        readyToJump = true;
        isGamePaused = false;
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void Update()
    {
        if (isGamePaused) return;

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        myInput();
        speedControll();

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    private void myInput()
    {
        horisontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            spawnBullet();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            UIPauseMenu.SetActive(true);
            isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = baseMoveSpeed * 2;
            runDust.startSpeed = 10f;
            anim.SetBool("IsSprinting", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = baseMoveSpeed;
            runDust.startSpeed = 5f;
            anim.SetBool("IsSprinting", false);
        }

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            jumpDust.Play();
            jump();

            Invoke(nameof(restartjump), jumpCooldown);
        }
    }

    private void movePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horisontalInput;

        if(moveDirection.magnitude <= 0)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            runDust.Stop();
            anim.SetBool("IsRuning", false);
        }
        else if (moveDirection.magnitude > 0 && grounded)
        {
            runDust.Play();
            anim.SetBool("IsRuning", true);
        }
        else
        {
            runDust.Stop();
            anim.SetBool("IsRuning", false);
        }

        if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void speedControll()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void spawnBullet()
    {
        Instantiate(bulletObject, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void restartjump()
    {
        readyToJump = true;
    }

    public float GetMovementMagnitude()
    {
        return verticalInput + horisontalInput;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        UIPauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
