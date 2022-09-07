using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private float _repeatRate = 2f;
    private Vector3 _spawnPosition = new Vector3(25, 0, 0);
    private bool _canSpawn = true;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_startDelay < 0f) _startDelay = 0f;
        if (_repeatRate < 0f) _repeatRate = 0f;
        if (transform.GetComponent<Pool>()) _pool = GetComponent<Pool>();
    }
    #endregion

    private void OnEnable()
    {
        Game.Instance.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        Game.Instance.OnGameOver -= StopSpawning;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), _startDelay, _repeatRate);
    }

    private void SpawnObstacle()
    {
        if (_canSpawn)
            _pool.CreateObject(_spawnPosition);
    }

    private void StopSpawning() => _canSpawn = false;
}
