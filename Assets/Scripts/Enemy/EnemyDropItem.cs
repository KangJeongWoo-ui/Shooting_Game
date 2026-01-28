using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    [SerializeField] private EnemyHealth enemyHealth;
    private void OnEnable()
    {
        enemyHealth.OnDie += DropItem;
    }
    private void DropItem()
    {
        int randDrop = Random.Range(0, 100);

        if(randDrop < 50)
        {
            Instantiate(items[0],transform.position, Quaternion.identity);
        }
        else if(randDrop < 70) 
        {
            Instantiate(items[1], transform.position, Quaternion.identity);
        }
        else if(randDrop < 90)
        {
            Instantiate(items[2], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("아이템 없음");
        }
    }
}
