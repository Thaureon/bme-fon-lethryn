using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float AscendSpeed = 5;
    public float RotationSpeed = 5;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crouchAction;

    //private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveValue = moveAction.ReadValue<Vector2>();

        var deltaSpeed = moveValue.y * Speed * Time.fixedDeltaTime;

        var ascendValue = jumpAction.IsPressed() ? 1 : 0;
        var descendValue = crouchAction.IsPressed() ? -1 : 0;

        var ascendSpeed = (ascendValue + descendValue) * AscendSpeed * Time.fixedDeltaTime;

        transform.position += gameObject.transform.forward * deltaSpeed + gameObject.transform.up * ascendSpeed;

        var cameraRotation = Quaternion.Euler(0.0f, moveValue.x * RotationSpeed * Time.fixedDeltaTime, 0.0f);

        transform.rotation *= cameraRotation;
    }
}
