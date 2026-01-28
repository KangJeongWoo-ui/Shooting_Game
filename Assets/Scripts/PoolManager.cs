using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [System.Serializable]
    public class PoolConfig
    {
        public GameObject prefab;
        public int initialSize = 10;
        public int maxSize = 20;   // 필요 없으면 크게
    }

    [Header("Index = Prefab Index")]
    [SerializeField] private PoolConfig[] pools;

    private readonly Dictionary<int, Queue<GameObject>> poolDict = new();
    private readonly Dictionary<GameObject, int> instanceToIndex = new();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        // 프리워밍
        for (int i = 0; i < pools.Length; i++)
        {
            poolDict[i] = new Queue<GameObject>(pools[i].initialSize);
            for (int n = 0; n < pools[i].initialSize; n++)
            {
                var go = CreateNew(i);
                Despawn(go);
            }
        }
    }

    private GameObject CreateNew(int index)
    {
        var cfg = pools[index];
        var go = Instantiate(cfg.prefab, transform);
        go.SetActive(false);
        instanceToIndex[go] = index;
        return go;
    }

    public GameObject Spawn(int index, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.TryGetValue(index, out var q))
        {
            q = new Queue<GameObject>();
            poolDict[index] = q;
        }

        GameObject go = null;

        // 비활성 오브젝트가 있으면 꺼내고, 없으면 생성
        while (q.Count > 0 && go == null)
            go = q.Dequeue();

        if (go == null)
        {
            // maxSize 체크를 엄격히 하려면 “현재 총 개수” 추적이 필요하지만,
            // 일단 기본은 계속 생성되는 방식으로 둬도 OK.
            go = CreateNew(index);
        }

        go.transform.SetPositionAndRotation(position, rotation);
        go.SetActive(true);

        if (go.TryGetComponent<IPoolable>(out var p))
            p.OnSpawned();

        return go;
    }

    public void Despawn(GameObject go)
    {
        if (go == null) return;

        if (!instanceToIndex.TryGetValue(go, out int index))
        {
            // 풀에서 만든 게 아니면 그냥 제거(혹은 무시)
            Destroy(go);
            return;
        }

        if (go.TryGetComponent<IPoolable>(out var p))
            p.OnDespawned();

        go.SetActive(false);
        go.transform.SetParent(transform);

        poolDict[index].Enqueue(go);
    }

    public void Despawn(Component c) => Despawn(c.gameObject);
}
