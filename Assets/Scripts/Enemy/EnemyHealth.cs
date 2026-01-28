using System;
using UnityEngine;

// 적 체력 스크립트
public class EnemyHealth : MonoBehaviour, IDamageable
{
    // 적 체력
    [SerializeField] private float hp;

    // 사망 상태 
    public bool IsDead { get; private set; }

    // 데미지를 받으면 호출되는 이벤트
    public event Action OnHit;

    // 풀레이어가 사망했을떄 호출되는 이벤트
    public event Action OnDie;
    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        // 체력 감소
        hp -= damage;

        // 피격 이벤트 호출
        OnHit?.Invoke();

        // 체력이 0이하가 되면 사망 처리
        if (hp <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        IsDead = true;

        // 사망 이벤트 호충
        OnDie?.Invoke();
    }
}
