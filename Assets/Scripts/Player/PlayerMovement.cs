using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _dirtParticle;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _crashSound;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isOnGround = true;
    private bool _canMove = true;
    private const float MultiplierJump = 10f;

    #region MonoBehaviour
    [System.Obsolete]
    private void OnValidate()
    {
        if (_jumpForce < 0f) _jumpForce = 0f;
        if (_gravityModifier < 0f) _gravityModifier = 0f;
        _explosionParticle = transform.FindChild("FX_Explosion_Smoke").GetComponent<ParticleSystem>();
        _dirtParticle = transform.FindChild("FX_DirtSplatter").GetComponent<ParticleSystem>();
    }
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        Physics.gravity *= _gravityModifier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround && _canMove)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce * MultiplierJump, ForceMode.Impulse);
        _isOnGround = false;
        _animator.SetTrigger("Jump_trig");
        _dirtParticle.Stop();
        _audioSource.PlayOneShot(_jumpSound, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _isOnGround = true;
            _dirtParticle.Play();
        }
        else if (collision.gameObject.GetComponent<ObstacleMovement>())
        {
            Death();
        }
    }

    private void Death()
    {
        _canMove = false;
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", 1);
        _explosionParticle.Play();
        Game.Instance.GameOver();
        _dirtParticle.Stop();
        _audioSource.PlayOneShot(_crashSound, 1f);
    }
}
