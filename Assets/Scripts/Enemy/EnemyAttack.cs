using UnityEngine;

// 적 공격 구현 스크립트
public class EnemyAttack : MonoBehaviour
{
    // 적 공격력
    [SerializeField] private int damage;

    private EnemyHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Player Tag에 닿으면
    // 플레이어의 IDamageable을 가져와 TakeDamage로 데미지를 줌
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyHealth.IsDead) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable player))
            {
                player.TakeDamage(damage);
            }
        }
    }
}
