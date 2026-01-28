using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDelay;

    private Rigidbody2D rb;

    private Vector2 dir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        dir = Vector2.down;
        StartCoroutine(StopMove(stopDelay));
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
    private IEnumerator StopMove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dir = Vector2.zero;
    }
}
