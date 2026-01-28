using UnityEngine;
using UnityEngine.UI;
public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] GameObject[] hpUI;
    [SerializeField] PlayerHealth playerHealth;

    private void OnEnable()
    {
        playerHealth.OnRemainHp += UpdateHpUI;
    }
    private void OnDisable()
    {
        playerHealth.OnRemainHp -= UpdateHpUI;
    }
    private void UpdateHpUI(int hp)
    {
        for(int i = 0; i < hpUI.Length; i++)
        {
            Image img = hpUI[i].GetComponent<Image>();
            img.enabled = (i < hp);
            continue;
        }
    }
}
