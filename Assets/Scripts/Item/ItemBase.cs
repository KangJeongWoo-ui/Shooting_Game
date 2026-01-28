using System;
using UnityEngine;

// 아이템 종류
public enum ItemType
{
    Boom,   // 폭탄
    Power,  // 파워업
    Coin    // 동전
}

// 아이템 스크립트
public class ItemBase : MonoBehaviour
{
    // 아이템 종류
    [SerializeField] private ItemType itemtype;

    // 아이템 이동 속도
    [SerializeField] private float moveSpeed;

    // 아이템을 먹을 떄 증가하는 횟수
    [SerializeField] private int amount = 1;

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

    // Player Tag에 닿으면
    // 플레이어의 PlayerInventory를 가져옴
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            Add(inventory, amount);
            PickUpItem();
        }
    }

    // 플레이어 인벤토리의 아이템 갯수를 늘리는 매서드
    private void Add(PlayerInventory inventory, int amount)
    {
        inventory.AddItem(itemtype, amount);
    }

    // 아이템 먹음 매서드
    private void PickUpItem()
    {
        Destroy(gameObject);
    }
}

