using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    public GameObject door1; // 첫 번째 문
    public GameObject door2; // 두 번째 문
    public float openAngle = 90f; // 문이 열릴 각도
    public float smooth = 2f; // 부드럽게 움직이는 속도

    private bool isOpen = false; // 문이 열려 있는지 여부
    private Quaternion door1ClosedRotation; // 첫 번째 문 닫힌 상태
    private Quaternion door1OpenRotation;   // 첫 번째 문 열린 상태
    private Quaternion door2ClosedRotation; // 두 번째 문 닫힌 상태
    private Quaternion door2OpenRotation;   // 두 번째 문 열린 상태

    [Header("Dependencies")]
    public GameObject calculator; // 계산기 오브젝트 (사용자가 지정)

    void Start()
    {
        // 첫 번째 문 초기 회전값 설정
        door1ClosedRotation = door1.transform.rotation;
        door1OpenRotation = Quaternion.Euler(
            door1.transform.eulerAngles.x,
            door1.transform.eulerAngles.y + openAngle, // 오른쪽 기준 바깥쪽으로 열림
            door1.transform.eulerAngles.z
        );

        // 두 번째 문 초기 회전값 설정
        door2ClosedRotation = door2.transform.rotation;
        door2OpenRotation = Quaternion.Euler(
            door2.transform.eulerAngles.x,
            door2.transform.eulerAngles.y - openAngle, // 오른쪽 기준 바깥쪽으로 열림
            door2.transform.eulerAngles.z
        );
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
                if (hit.transform == this.transform) // 클릭한 오브젝트가 캐비넷인지 확인
                {
                    // Calculator가 사라진 경우에만 문 상태를 전환
                    if (calculator == null || !calculator.activeSelf)
                    {
                        isOpen = !isOpen; // 문 상태 전환
                    }
                    else
                    {
                        Debug.Log("계산기가 아직 활성화 상태입니다. 문을 열 수 없습니다.");
                    }
                }
            }
        }

        // 첫 번째 문 회전 업데이트
        if (isOpen)
        {
            door1.transform.rotation = Quaternion.Slerp(door1.transform.rotation, door1OpenRotation, Time.deltaTime * smooth);
            door2.transform.rotation = Quaternion.Slerp(door2.transform.rotation, door2OpenRotation, Time.deltaTime * smooth);
        }
        else
        {
            door1.transform.rotation = Quaternion.Slerp(door1.transform.rotation, door1ClosedRotation, Time.deltaTime * smooth);
            door2.transform.rotation = Quaternion.Slerp(door2.transform.rotation, door2ClosedRotation, Time.deltaTime * smooth);
        }
    }
}
