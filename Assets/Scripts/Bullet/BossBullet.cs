using UnityEngine;

public class BossBullet : MonoBehaviour
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
        rb.linearVelocity = (Vector2)transform.up * moveSpeed;
    }

    // 총알이 플레이어 Tag에 닿으면
    // 플레이어의 IDamageable을 가져와 TakeDamage로 데미지를 주고
    // 총알 오브젝트를 삭제함
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable player))
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
