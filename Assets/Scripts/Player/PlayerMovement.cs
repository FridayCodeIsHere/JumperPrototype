using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _gravityModifier = 1f;
    private Rigidbody _rigidbody;
    private bool _isOnGround = true;
    private const float MultiplierJump = 10f;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_jumpForce < 0f) _jumpForce = 0f;
        if (_gravityModifier < 0f) _gravityModifier = 0f;
    }
    #endregion

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= _gravityModifier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce * MultiplierJump, ForceMode.Impulse);
        _isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _isOnGround = true;
        }
        else if (collision.gameObject.GetComponent<ObstacleMovement>())
        {
            Game.Instance.GameOver();
        }
    }
}
