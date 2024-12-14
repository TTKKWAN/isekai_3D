using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // 문이 열릴 각도
    public float closeAngle = 0f; // 문이 닫힐 각도
    public float smooth = 2f; // 부드럽게 움직이는 속도

    private bool isOpen = false; // 문이 열려 있는지 여부
    private Quaternion closedRotation; // 닫힌 상태의 회전값
    private Quaternion openRotation;   // 열린 상태의 회전값
    [SerializeField] private GameObject Door;

    void Start()
    {
        // 문 초기 회전값 설정
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);
    }

    void Update()
    {
        // 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭
        {
            
            // 화면 중앙에서 Ray 발사
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform == transform) // 클릭한 오브젝트가 문인지 확인
                {
                    AudioSource Doorsound = Door.GetComponent<AudioSource>();

                    if (Doorsound != null) {
                        Doorsound.Play();
                    }
                    isOpen = !isOpen; // 문 상태 전환
                }
            }
        }

        // 문 회전 업데이트
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * smooth);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * smooth);
        }
    }
}
