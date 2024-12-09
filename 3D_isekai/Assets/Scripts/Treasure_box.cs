using UnityEngine;

public class BoxLidController : MonoBehaviour
{
    public GameObject lid; // 뚜껑 오브젝트
    public float openAngle = 90f; // 뚜껑이 열릴 각도
    public float speed = 2f; // 열리는 속도

    public GameObject showOnOpen; // 뚜껑이 열리면 보이게 할 오브젝트
    public GameObject hideOnOpen; // 뚜껑이 열리면 숨길 오브젝트

    public bool IsLidOpen()
    {
        return isOpen; // 뚜껑이 열려 있는 상태를 반환
    }

    private bool isOpen = false; // 현재 열림 상태
    private Quaternion closedRotation; // 닫힌 상태의 회전
    private Quaternion openRotation; // 열린 상태의 회전
    private BoxCollider thisCollider; // 현재 오브젝트의 Box Collider
    private Rigidbody thisRigidbody; // 현재 오브젝트의 Rigidbody

    void Start()
    {
        // 초기 상태 저장
        closedRotation = lid.transform.rotation;
        openRotation = Quaternion.Euler(openAngle, lid.transform.rotation.eulerAngles.y, lid.transform.rotation.eulerAngles.z);

        // 현재 오브젝트의 Box Collider와 Rigidbody 가져오기
        thisCollider = GetComponent<BoxCollider>();
        thisRigidbody = GetComponent<Rigidbody>();

        if (thisCollider == null)
        {
            Debug.LogError("Box Collider not found on this object!");
        }

        if (thisRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on this object!");
        }

        // 시작 시 오브젝트 상태 초기화
        if (showOnOpen != null) showOnOpen.SetActive(false); // 기본적으로 숨김
        if (hideOnOpen != null) hideOnOpen.SetActive(true);  // 기본적으로 보이도록 설정
    }

    void OnMouseDown()
    {
        // 뚜껑 열림 상태로 변경
        if (!isOpen) // 한 번만 실행
        {
            isOpen = true;

            // Box Collider와 Rigidbody 비활성화
            if (thisCollider != null)
            {
                thisCollider.enabled = false; // Box Collider 비활성화
            }

            if (thisRigidbody != null)
            {
                thisRigidbody.isKinematic = true; // Rigidbody를 비활성화
                thisRigidbody.detectCollisions = false; // 충돌 감지 비활성화
            }

            // 오브젝트 상태 변경
            if (showOnOpen != null) showOnOpen.SetActive(true);  // 보이게 설정
            if (hideOnOpen != null) hideOnOpen.SetActive(false); // 숨기기 설정
        }
    }

    void Update()
    {
        // 뚜껑 회전 애니메이션
        if (isOpen)
        {
            lid.transform.rotation = Quaternion.Slerp(lid.transform.rotation, openRotation, Time.deltaTime * speed);
        }
    }
}
