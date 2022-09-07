using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _startPosition;
    private float _repeatWidth = 30f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        _startPosition = _transform.position;
    }

    private void Update()
    {
        if (_transform.position.x < _startPosition.x - _repeatWidth)
        {
            _transform.position = _startPosition;
        }
    }
}
