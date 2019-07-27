using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    public Vector2 turnSpeed = new Vector2(1, 1);
    public bool invertY;
    Quaternion _initialOrientation;
    Vector2 _currentAngles;
    CursorLockMode _previousLockState;
    bool _wasCursorVisible;
    private Vector3 moveDirection = Vector3.zero;



    void Awake()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }


    void Update()
    {
        //Every frame, run our Mouse Look and Keyboard Movement logic functions
        LookLogic();
    }

    void OnEnable()
    {
        // Cache our starting orientation as our center point.
        _initialOrientation = transform.localRotation;

        // Cache the previous cursor state so we can restore it later.
        _previousLockState = Cursor.lockState;
        _wasCursorVisible = Cursor.visible;

        // Hide & lock the cursor for that FPS experience
        // and to avoid distractions / accidental clicks
        // from the mouse cursor moving around.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // When switched off, put everything back the way we found it.
        Cursor.visible = _wasCursorVisible;
        Cursor.lockState = _previousLockState;
        transform.localRotation = _initialOrientation;
    }

    public void LookLogic()
    {
        // Collect relative motion of mouse since last frame.
        Vector2 motion = new Vector2(
                            Input.GetAxis("Mouse X"),
                            Input.GetAxis("Mouse Y"));

        // Scale it by the turn speed, add it to our current angle, and clamp.
        motion = Vector2.Scale(motion, turnSpeed);
        _currentAngles += motion;

        // Rotate to look in this direction, relative to our initial orientation.
        Quaternion look = Quaternion.Euler(
                            -_currentAngles.y,                       // Yaw
                            (invertY ? -1f : 1f) * _currentAngles.x, // Pitch
                            0);                                      // Roll

        transform.localRotation = _initialOrientation * look;
    }
}
