using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 3;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private GameObject _prefab;

    private PoolMono _pool;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_poolCount < 0) _poolCount = 0;
    }
    #endregion

    private void Start()
    {
        _pool = new PoolMono(_prefab, _poolCount, this.transform);
        _pool.IsAutoExpand = _autoExpand;
    }

    public void CreateObject(Vector3 position)
    {
        GameObject item = _pool.GetFreeElement();
        item.transform.position = position;
    }
}
