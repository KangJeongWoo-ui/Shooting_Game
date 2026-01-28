using System;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public event Action OnBossDie;
    protected override void Die()
    {
        base.Die();
        OnBossDie?.Invoke();
    }
}
