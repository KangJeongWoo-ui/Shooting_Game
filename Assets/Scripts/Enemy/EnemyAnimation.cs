using UnityEngine;

// 적 애니매이션 스크립트
public class EnemyAnimation : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private Animator anim;
    private void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        enemyHealth.OnHit += HitAnim;
        enemyHealth.OnDie += DieAnim;
    }
    private void OnDisable()
    {
        enemyHealth.OnHit -= HitAnim;
        enemyHealth.OnDie -= DieAnim;
    }
    private void HitAnim()
    {
        anim.SetTrigger("doHit");
    }
    private void DieAnim()
    {
        anim.SetTrigger("doDie");
    }
    public void DestroyEnemy()
    {
        Destroy(transform.root.gameObject);
    }
}
