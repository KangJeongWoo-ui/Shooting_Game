using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackInterval;

    private float attackTimer;
    private void Update()
    {
        attackTimer += Time.deltaTime;

        // 설정한 시간이 지났으면 총알을 발사하고 타이머 리셋
        if (attackTimer >= attackInterval)
        {
            Fire();
            attackTimer = 0f;
        }
    }
    private void Fire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
