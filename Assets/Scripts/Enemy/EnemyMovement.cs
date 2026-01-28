using UnityEngine;
using UnityEngine.InputSystem.iOS;

// 적 움직임 스크립트
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;

    private Vector2 dir = Vector2.down;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }

    // 움직임 매서드
    // 아래 방향으로 이동
    private void Move()
    {
        rb.linearVelocity = dir * moveSpeed;
    }
}
