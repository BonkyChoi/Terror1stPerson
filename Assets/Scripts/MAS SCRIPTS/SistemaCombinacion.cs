using System.Collections.Generic;
using UnityEngine;

public class SistemaCombinacion : MonoBehaviour
{
    [System.Serializable]
    public class Combination
    {
        public GameObject prefabA;
        public GameObject prefabB;
        public GameObject result;
    }

    [System.Serializable]
    public class Item
    {
        public GameObject instance;
    }

    [Header("Combinaciones")]
    public List<Combination> combinaciones;

    
    public Transform spawnPoint;

    private List<Item> itemsInside = new List<Item>();

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (!ContainsInstance(obj))
        {
            itemsInside.Add(new Item { instance = obj });
        }

        TryCombine();
    }
    private void OnTriggerExit(Collider other)
    {
        RemoveInstance(other.gameObject);
    }
    private void TryCombine()
    {
        if (itemsInside.Count < 2) return;

        for (int i = 0; i < itemsInside.Count; i++)
        {
            for (int j = i + 1; j < itemsInside.Count; j++)
            {
                GameObject a = itemsInside[i].instance;
                GameObject b = itemsInside[j].instance;

                if (a == null || b == null) continue;

                foreach (var combo in combinaciones)
                {
                    if (Match(a, combo.prefabA) && Match(b, combo.prefabB) ||
                        Match(a, combo.prefabB) && Match(b, combo.prefabA))
                    {
                        Vector3 spawnPos = spawnPoint != null
                            ? spawnPoint.position
                            : (a.transform.position + b.transform.position) / 2f;

                        Instantiate(combo.result, spawnPos, Quaternion.identity);

                        Destroy(a);
                        Destroy(b);

                        RemoveInstance(a);
                        RemoveInstance(b);

                        return;
                    }
                }
            }
        }
    }
    private bool Match(GameObject obj, GameObject prefab)
    {
        if (obj == null || prefab == null) return false;

        return obj.name.Replace("(Clone)", "").Trim() == prefab.name;
    }
    private bool ContainsInstance(GameObject obj)
    {
        foreach (var item in itemsInside)
        {
            if (item.instance == obj)
                return true;
        }
        return false;
    }
    private void RemoveInstance(GameObject obj)
    {
        for (int i = 0; i < itemsInside.Count; i++)
        {
            if (itemsInside[i].instance == obj)
            {
                itemsInside.RemoveAt(i);
                return;
            }
        }
    }
}