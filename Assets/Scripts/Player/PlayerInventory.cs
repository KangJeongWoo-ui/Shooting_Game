using System;
using UnityEngine;

// 플레이어가 가진 아이템 관리 스크립트
public class PlayerInventory : MonoBehaviour
{
    public int boomCount {  get; private set; }
    public int powerLevel { get; private set; }
    public int coinCount { get; private set; }

    private int maxBoomCount = 3;

    public event Action<int> OnBoomCount;
    public event Action<int> OnPowerLevel;
    public event Action<int> OnCoinCount;

    public void AddItem(ItemType type, int amount)
    {
        switch(type)
        {
            case ItemType.Boom:
                boomCount = Mathf.Clamp(boomCount + amount, 0, maxBoomCount);
                OnBoomCount?.Invoke(boomCount);
                break;
            case ItemType.Power:
                powerLevel += amount;
                OnPowerLevel?.Invoke(powerLevel);
                break;
            case ItemType.Coin:
                coinCount += amount;
                OnCoinCount?.Invoke(coinCount);
                break;
        }
    }
    public bool UseBoom()
    {
        if(boomCount <= 0) return false;

        boomCount--;
        OnBoomCount?.Invoke(boomCount);
        return true;
    }
}
