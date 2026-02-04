using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float RotationSpeed = 5;

    private InputAction moveAction;

    [SerializeField]
    private AnimationCurve AnimCurve;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveValue = moveAction.ReadValue<Vector2>();

        // Cast a ray downwards from the object's position
        var ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var deltaSpeed = moveValue.y * Speed * Time.fixedDeltaTime;
            rb.MovePosition(new Vector3(rb.position.x, hit.point.y + 1.0f, rb.position.z) + gameObject.transform.forward * deltaSpeed);

            var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            var rotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal),
                AnimCurve.Evaluate(0.25f));

            Debug.Log(slopeAngle);
            Debug.Log(hit.normal);
            Debug.Log(transform.up);
            Debug.Log(rotationRef);

            var deltaRotation = Quaternion.Euler(rotationRef.eulerAngles.x, moveValue.x * RotationSpeed * Time.fixedDeltaTime, rotationRef.eulerAngles.z);

            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
