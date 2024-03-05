using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    public float jumpVelocity = 5f;
    private bool canJump = false;
    private bool canShoot = false;
    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    
    private float vInput;
    private float hInput;

    
    private Rigidbody _rb;

    private CapsuleCollider _col;

    private gameBehaviour gameManager;

    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;
    public bool lockCursor = true;
    public float pitch = 0.0f;
    public float yaw = 0.0f;
    public Camera Main_Camera;

    public delegate void JumpingEvent();

    public event JumpingEvent playerJump;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        _col = GetComponent<CapsuleCollider>();

        _rb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<gameBehaviour>();

    }

    
    void Update()
    {
        
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        hInput = Input.GetAxis("Horizontal") * moveSpeed;

        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            canShoot = true;
        }
        

        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            Main_Camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

    
    void FixedUpdate()
    {
        
        _rb = GetComponent<Rigidbody>();
        _rb.MovePosition(transform.position + (transform.forward * vInput + this.transform.right * hInput) * Time.fixedDeltaTime);

        if (canJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            canJump = false;
        }

        if (canShoot)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + Main_Camera.transform.forward, this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;

            canShoot = false;

            playerJump();
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            gameManager.HP -= 1;
        }
    }
}
