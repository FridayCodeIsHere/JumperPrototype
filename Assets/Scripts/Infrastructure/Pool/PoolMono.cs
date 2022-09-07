using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono
{
    public GameObject Prefab { get; }
    public bool IsAutoExpand { get; set; }
    public Transform Container { get; }
    private List<GameObject> _pool;

    public PoolMono(GameObject prefab, int count, Transform container = null)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject(bool isActive = false)
    {
        GameObject createdObject = GameObject.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActive);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out GameObject element)
    {
        foreach (GameObject item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public GameObject GetFreeElement()
    {
        if (HasFreeElement(out GameObject element))
        {
            return element;
        }
        if (IsAutoExpand)
        {
            return CreateObject(true);
        }
        return null;
    }
}
