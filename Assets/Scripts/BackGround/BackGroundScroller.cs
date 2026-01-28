using UnityEngine;

// 배경 무한 스크롤 스크립트
public class BackGroundScroller : MonoBehaviour
{
    // 배경 Transform 배열
    [SerializeField] private Transform[] backGrounds;

    // 배경이 아래로 이동하는 속도
    [SerializeField] private float backGroundSpeed;

    // 이 Y좌표 이하로 내려가면 배경이 재배치 됨
    [SerializeField] private float resetY;

    // 이 Y좌표를 더한 값만큼 배경이 재배치 됨
    [SerializeField] private float resetOffsetY;

    private void Update()
    {
        MoveBackGround();
        RepositionBackground();
    }

    // 배경을 아래 방향으로 이동시킨다
    private void MoveBackGround()
    {
        // 배경 이동 속도 x deltTime
        float deltaMovement = backGroundSpeed * Time.deltaTime;

        // 모든 배경을 아래 방향으로 이동
        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].position += Vector3.down * deltaMovement;
        }
    }

    // resetY 값 이하로 내려간 배경을 재배치 시킨다
    private void RepositionBackground()
    {
        // 가장 위에 있는 배경의 y좌표 값을 구함
        float topBackGround = backGrounds[0].position.y;

        // 모든 배경을 순회하며 가장 큰 y값(가장 위에 있는 배경)을 찾는다
        for (int i=0; i < backGrounds.Length;i++)
        {
            if (backGrounds[i].position.y > topBackGround)
            {
                topBackGround = backGrounds[i].position.y;
            }
        }

        for (int i = 0;i < backGrounds.Length;i++)
        {
            // resetY 값 이하로 내려간 배경은
            // 가장 위에 있는 배경 위로 resetOffsetY 값을 더한 만큼 올린다
            if(backGrounds[i].position.y <= resetY)
            {
                backGrounds[i].position = new Vector3(
                    backGrounds[i].position.x, 
                    topBackGround + resetOffsetY, 
                    backGrounds[i].position.z);
            }
        }
    }
}
