using UnityEditor.EditorTools;
using UnityEngine;

// 설정한 좌표값을 넘어가면 오브젝트를 삭제시키는 스크립트
public class Despawn : MonoBehaviour
{
    private float MaxY = 9f;
    private float MaxX = 5f;
    private float MinY = -7f;
    private float MinX = -5f;

    private void LateUpdate()
    {
        DespawnObject();
    }
    private void DespawnObject()
    {
        if( transform.position.y > MaxY || 
            transform.position.y < MinY || 
            transform.position.x > MaxX || 
            transform.position.x < MinX)
        {
            Destroy(gameObject);
        }
    }
}
