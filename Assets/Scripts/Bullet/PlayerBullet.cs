using UnityEngine;

// 플레이어 총알 스크립트
public class PlayerBullet : MonoBehaviour
{
    // 총알 데미지
    [SerializeField] private int damage;

    // 총알 이동 속도
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }

    // 총알 움직임 매서드
    // 윗방향으로 이동
    private void Move()
    {
        rb.linearVelocity = Vector2.up * moveSpeed;
    }

    // 총알이 적 Tag에 닿으면
    // 적의 IDamageable을 가져와 TakeDamage로 데미지를 주고
    // 총알 오브젝트를 삭제함
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent<IDamageable>(out IDamageable enemy))
            {
                enemy.TakeDamage(damage);
            }
            //Destroy(gameObject);
            ObjectPoolManager.Instance.Despawn(this);
        }
    }
    public void OnSpawned()
    {
        // 풀에서 꺼낼 때 초기화(필요한 것만)

        // 혹시 물리 잔상 방지
        if (rb) rb.linearVelocity = Vector2.zero;
    }

    public void OnDespawned()
    {
        // 풀로 돌아갈 때 정리(필요한 것만)
        if (rb) rb.linearVelocity = Vector2.zero;
    }
}
