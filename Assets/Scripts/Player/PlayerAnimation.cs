using UnityEngine;
using UnityEngine.EventSystems;

// 플레이어 애니매이션 스크립트
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private InputController inputController;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerHealth = GetComponentInParent<PlayerHealth>();
        inputController = GetComponentInParent<InputController>();
    }
    private void OnEnable()
    {
        // 이동 입력 이벤트 구독
        inputController.OnMoveEvent += Move;

        // 플레이어 사망 이벤트 구독
        PlayerHealth.OnDie += Die;

        playerHealth.OnInvincibleChanged += Invincible;
    }
    private void OnDisable()
    {
        inputController.OnMoveEvent -= Move;

        PlayerHealth.OnDie -= Die;

        playerHealth.OnInvincibleChanged -= Invincible;
    }
    private void Move(Vector2 dir)
    {
        anim.SetInteger("doMove", dir.x > 0 ? 1 : (dir.x < 0 ? -1 : 0));
    }
    private void Die()
    {
        anim.SetTrigger("doDie");
    }
    private void Invincible(bool value)
    {
        anim.SetBool("doInvincible", value);
    }
    public void DestroyPlayer()
    {
        Destroy(transform.root.gameObject);
    }
}
