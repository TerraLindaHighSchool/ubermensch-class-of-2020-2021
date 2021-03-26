using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;
    Rigidbody jump_rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;


    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        if (!groundCheck)
            groundCheck = GroundCheck.Create(transform);
    }

    void Awake()
    {
        jump_rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
        {
            jump_rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
