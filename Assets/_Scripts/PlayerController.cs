using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jump;
    public float speed;
    public float jTime;
    public float jumpTime;

    public float gravity;


    CharacterController controller;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fall();
        if (Input.GetKey(KeyCode.Space) && Time.time < jumpTime)
        {
            gravity = 0;
            controller.Move(Vector3.up * jump * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.Space) && Time.time > jumpTime)
        {
            gravity = 1;
        }
        else
        {
            gravity = 5;
        }

    }

    void Fall()
    {
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * gravity * Time.deltaTime);
        }
        if(controller.isGrounded)
        {
            jumpTime = Time.time + jTime;
        }
    }

    void Move()
    {
        //bool dodge = false;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
