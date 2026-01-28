using UnityEngine;

// 플레이어 이동 스크립트
public class PlayerMovement : MonoBehaviour
{
    // 플레이어 이동 속도
    [SerializeField] private float moveSpeed;

    // 플레이어 이동 방향
    private Vector2 moveDirection = Vector2.zero;

    // 각 경계에 접촉 중인지 확인
    private bool touchTop;        
    private bool touchBottom;
    private bool touchRight;
    private bool touchLeft;

    private Rigidbody2D rb;
    private InputController inputController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
    }
    private void Start()
    {
        // 이동 입력 이벤트 구독
        inputController.OnMoveEvent += Move;
    }
    private void FixedUpdate()
    {
        Vector2 dir = Apply(moveDirection).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    // 입력 이벤트로 전달된 이동 방향을 저장
    private void Move(Vector2 dir)
    {
        moveDirection = dir;
    }

    // 경계에 들어가면 접촉 상태를 true로 설정
    private void OnTriggerEnter2D(Collider2D other)
    {
        SetBorderState(other, true);
    }

    // 경계에 나가면 접촉 상태를 false로 설정
    private void OnTriggerExit2D(Collider2D other)
    {
        SetBorderState(other, false);
    }

    // 경계에 접촉 상태 갱신
    private void SetBorderState(Collider2D other, bool state)
    {
        if(other.CompareTag("TopBorder")) touchTop = state;
        else if(other.CompareTag("BottomBorder")) touchBottom = state;
        else if(other.CompareTag("RightBorder")) touchRight = state;
        else if (other.CompareTag("LeftBorder")) touchLeft = state;
    }

    // 경계 접촉 상태에 따라 이동 입력을 차단
    private Vector2 Apply(Vector2 dir)
    {
        // 위쪽 경계에 닿은 상태에서 위로 이동하려 하면 y값을 0으로 차단
        if (touchTop && dir.y > 0f) dir.y = 0f;

        // 아래쪽 경계에 닿은 상태에서 아래로 이동하려 하면 y값을 0으로 차단
        if (touchBottom && dir.y < 0f) dir.y = 0f;

        // 오른쪽 경계에 닿은 상태에서 오른쪽으로 이동하려 하면 x값을 0으로 차단
        if (touchRight && dir.x > 0f) dir.x = 0f;

        // 왼쪽 경계에 닿은 상태에서 왼쪽으로 이동하려 하면 x값을 0으로 차단
        if (touchLeft && dir.x < 0f) dir.x = 0f;

        return dir;
    }
}
