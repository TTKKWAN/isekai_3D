using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // 문이 열리고 닫히는 속도
    public float openSpeed = 2.0f;

    // 문이 열릴 각도 (예: 90도)
    public float openAngle = 90f;

    // 문이 닫힌 상태의 회전값 저장
    private Quaternion closedRotation;

    // 문이 열린 상태의 회전값 저장
    private Quaternion openRotation;

    // 문이 열려 있는지 여부
    private bool isOpen = false;
    public Collider doorCollider; // 문에 있는 Collider

    private void Start()
    {
        // 문이 닫힌 초기 회전값 저장
        closedRotation = transform.rotation;

        // 문이 열린 회전값 계산
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    private void OnMouseDown()
    {
        // 문 클릭 시 상태 변경
        isOpen = !isOpen;
    }



    private void Update()
    {
        // 문이 열리거나 닫히는 동작 처리
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
            doorCollider.isTrigger = true; // 문 열리면 Trigger 활성화
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
            doorCollider.isTrigger = false; // 문 닫히면 Trigger 비활성화
        }
    }
}
