using UnityEngine;
using UnityEngine.UI;
public class PlayerBoomUI : MonoBehaviour
{
    [SerializeField] GameObject[] boomUI;
    [SerializeField] PlayerInventory playerInventory;

    private void OnEnable()
    {
        playerInventory.OnBoomCount += UpdateBoomUI;
        UpdateBoomUI(playerInventory.boomCount);
    }
    private void OnDisable()
    {
        playerInventory.OnBoomCount -= UpdateBoomUI;
    }
    private void UpdateBoomUI(int count)
    {
        for (int i = 0; i < boomUI.Length; i++)
        {
            Image img = boomUI[i].GetComponent<Image>();
            img.enabled = (i < count);
            continue;
        }
    }
}
