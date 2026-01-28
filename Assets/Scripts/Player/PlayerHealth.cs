using System;
using System.Collections;
using UnityEngine;

// 플레이어 체력을 관리하는 스크립트
public class PlayerHealth : MonoBehaviour, IDamageable
{
    // 플레이어 체력
    [SerializeField] private int hp;
    [SerializeField] private float invincibleTime;

    // 사망 상태 
    public bool IsDead {  get; private set; }

    // 무적 상태
    public bool isInvincible { get; private set; }

    // 데미지를 받으면 호출되는 이벤트
    public event Action OnHit;

    // 풀레이어가 사망했을떄 호출되는 이벤트
    public static event Action OnDie;

    // 플레이어의 남은 체력을 호출하는 이벤트
    public event Action<int> OnRemainHp;

    // 플레이어 무적 상태를 호출하는 이벤트
    public event Action<bool> OnInvincibleChanged;

    public void TakeDamage(int damage)
    {
        if(IsDead) return;
        if (isInvincible) return;

        // 체력 감소
        hp -= damage;

        // 남은 체력 이벤트 호출
        OnRemainHp?.Invoke(hp);

        // 피격 이벤트 호출
        OnHit?.Invoke();
        
        // 체력이 0이하가 되면 사망 처리
        if(hp <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(Invincible(invincibleTime));
    }
    private void Die()
    {
        IsDead = true;

        // 사망 이벤트 호충
        OnDie?.Invoke();
    }
    private IEnumerator Invincible(float time)
    {
        isInvincible = true;
        OnInvincibleChanged?.Invoke(true);

        yield return new WaitForSeconds(time);

        isInvincible = false;
        OnInvincibleChanged?.Invoke(false);
    }
}
