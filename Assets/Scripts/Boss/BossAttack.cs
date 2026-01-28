using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject[] bossBulletPrefabs;
    [SerializeField] private BossPattern[] patterns;

    private Coroutine pattern;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayPattern(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayPattern(1);
    }
    public GameObject GetBullet(int index)
    {
        index = Mathf.Clamp(index, 0, bossBulletPrefabs.Length - 1);
        return bossBulletPrefabs[index];
    }
    public void PlayPattern(int index)
    {
        index = Mathf.Clamp(index, 0, patterns.Length - 1);
        StopPattern();
        pattern = StartCoroutine(patterns[index].Execute(this));
    }
    public void StopPattern()
    {
        if(pattern != null)
        {
            StopCoroutine(pattern);
            pattern = null;
        }
    }
    public void Fire(int bulletIndex, Vector3 pos, Quaternion rot)
    {
        bulletIndex = 2;
        ObjectPoolManager.Instance.Spawn(bulletIndex, pos, rot);
    }
}
