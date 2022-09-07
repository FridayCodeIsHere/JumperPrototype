using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private PlayerMovement _playerMovement;
    private Transform _transform;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_speed < 0f) _speed = 0f;
    }
    #endregion


    private void OnEnable()
    {
        Game.Instance.OnGameOver += StopMove;
    }

    private void OnDisable()
    {
        Game.Instance.OnGameOver -= StopMove;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        _transform.Translate(Vector3.left * _speed * Time.deltaTime);
        
    }

    private void StopMove()
    {
        _speed = 0f;
    }
}
