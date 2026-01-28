using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    // ÆøÅº µ¥¹ÌÁö
    [SerializeField] private int damage;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // ÆøÅºÀÌ Àû Tag¿¡ ´êÀ¸¸é
    // ÀûÀÇ IDamageableÀ» °¡Á®¿Í TakeDamage·Î µ¥¹ÌÁö¸¦ ÁÜ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    public void OnBoomEffectEnd()
    {
        Destroy(gameObject); 
    }
}
