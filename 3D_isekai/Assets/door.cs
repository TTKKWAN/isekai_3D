using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f; // 문이 열릴 각도
    public float openSpeed = 2f;  // 문이 열리는 속도
    private bool isOpen = false;  // 문이 열렸는지 상태 확인
    private Quaternion closedRotation; // 문 닫힌 상태 회전값
    private Quaternion openRotation;   // 문 열린 상태 회전값

    void Start()
    {
        // 초기 닫힌 상태와 열린 상태 회전값 설정
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0); // y축 기준으로 열기
    }

    void Update()
    {
        // 클릭 이벤트 처리
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 화면에서 클릭한 위치로 Ray 생성
            if (Physics.Raycast(ray, out RaycastHit hit, 100f)) // Ray가 Collider에 맞았는지 확인
            {
                if (hit.collider.gameObject == this.gameObject) // 문을 클릭했는지 확인
                {
                    isOpen = !isOpen; // 문 상태 토글
                }
            }
        }

        // 문을 열거나 닫기
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
