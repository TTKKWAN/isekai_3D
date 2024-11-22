using UnityEngine;

public class HiddenWallController : MonoBehaviour
{
    public float slideDistance = 5f; // 벽이 슬라이딩할 거리
    public float slideSpeed = 2f;    // 슬라이딩 속도

    private Vector3 initialPosition; // 벽의 초기 위치
    private Vector3 targetPosition;  // 벽이 슬라이딩할 목표 위치
    private bool isSliding = false;  // 슬라이딩 상태 여부

    private Transform parentObject; // 클릭한 오브젝트의 부모 저장

    void Start()
    {
        // 벽이 Z축 방향(뒤로) 슬라이딩하도록 설정
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.forward * slideDistance; // Z축 음수 방향(뒤로)
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray 발사
            int layerMask = LayerMask.GetMask("wall layer"); // LayerMask 설정

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                // 클릭한 오브젝트의 바로 위 부모 찾기
                parentObject = hit.transform.parent; // 직속 부모 오브젝트

                if (parentObject != null && parentObject.GetComponent<HiddenWallController>() != null)
                {
                    // 부모 오브젝트에서 HiddenWallController 컴포넌트 실행
                    parentObject.GetComponent<HiddenWallController>().StartSliding();
                }
            }
        }
    }

    // 슬라이딩 시작 함수
    public void StartSliding()
    {
        isSliding = true;
    }

    void FixedUpdate()
    {
        if (isSliding) // 슬라이딩 동작
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isSliding = false;
            }
        }
    }
}
