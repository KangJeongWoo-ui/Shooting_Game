
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletSettings
{
    public int bulletPrefabIndex;
    public Vector3 offset;
}
[Serializable]
public class FirePattern
{
    public List<BulletSettings> settings = new List<BulletSettings>();
}
// 플레이어 공격 스크립트
public class PlayerAttack : MonoBehaviour
{
    // 총알 프리팹
    [SerializeField] private GameObject[] bulletPrefabs;

    // 폭탄 효과 프리팹
    [SerializeField] private GameObject boomEffectPrefab;

    // 연사 간격
    [SerializeField] private float attackInterval;

    [SerializeField] private List<FirePattern> patterns = new List<FirePattern>();

    // 현재 공격 버튼이 눌린 상태인지 확인 여부
    private bool isAttacking;

    // 다음 발사까지 누적되는 시간
    private float attackTimer;

    private InputController inputController;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        playerInventory = GetComponent<PlayerInventory>();
    }
    private void Start()
    {
        // 공격 입력 이벤트 구독
        inputController.OnFireEvent += OnFireInput;

        inputController.OnBoomEvent += OnBoomInput;
    }
    private void Update()
    {
        // 공격 중이 아니라면 return
        if (!isAttacking) return;

        attackTimer += Time.deltaTime;

        // 설정한 시간이 지났으면 총알을 발사하고 타이머 리셋
        if (attackTimer >= attackInterval && isAttacking)
        {
            Fire();
            attackTimer = 0f;
        }
    }
    private void OnFireInput(bool pressed)
    {
        // 입력 상태에 따라서 공격 중 활성화, 비활성화
        isAttacking = pressed;
    }

    // 총알 발사 메서드
    private void Fire()
    {
        int powerLevel = Mathf.Clamp(playerInventory.powerLevel, 0 ,patterns.Count - 1);
        FirePattern pattern = patterns[powerLevel];

        foreach(BulletSettings bs in pattern.settings)
        {
            GameObject bullet = bulletPrefabs[bs.bulletPrefabIndex];

            Vector3 pos = transform.position + bs.offset;
            Quaternion rot = Quaternion.identity;

            //Instantiate(bullet, pos, rot);
            ObjectPoolManager.Instance.Spawn(bs.bulletPrefabIndex, pos, rot);
        }
    }
    private void OnBoomInput(bool pressed)
    {
        if(playerInventory.UseBoom())
        {
            Instantiate(boomEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
