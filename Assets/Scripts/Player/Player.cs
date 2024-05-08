using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3   _velocity  ;
    private bool      _isGrounded;
    private Rigidbody _rigidBody ;

    [SerializeField]
    float _jumpForce = 20.0f;

    private void Start()
    {
        _rigidBody  = GetComponent<Rigidbody>();
        _isGrounded = true;
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector3(_velocity.x * 5.0f, _rigidBody.velocity.y, _velocity.z * 5.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!_isGrounded || context.canceled)
        {
            return;
        }

        Debug.Log(_rigidBody.velocity.y);
        _rigidBody.AddForce(Vector3.up * _jumpForce);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        _velocity = transform.right * movement.x + transform.forward * movement.y;
    }
}
