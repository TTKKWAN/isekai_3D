using UnityEngine;

public class button_door : MonoBehaviour
{
    public GameObject door; // 열릴 문 오브젝트
    public GameObject lid; // 뚜껑 오브젝트
    public float openAngle = 90f; // 문이 열릴 각도
    public float speed = 3f; // 문이 열리는 속도
    private bool isOpen = false; // 문이 열렸는지 상태 확인
    private Quaternion closedRotation; // 문이 닫힌 상태의 회전 값
    private Quaternion openRotation; // 문이 열린 상태의 회전 값
    private BoxLidController lidController; // 뚜껑 상태 확인용 스크립트

    void Start()
    {
        // 초기 상태 저장
        closedRotation = door.transform.rotation;
        openRotation = Quaternion.Euler(door.transform.rotation.eulerAngles.x, door.transform.rotation.eulerAngles.y + openAngle, door.transform.rotation.eulerAngles.z);

        // 뚜껑 상태 확인용 스크립트 가져오기
        lidController = lid.GetComponent<BoxLidController>();
        if (lidController == null)
        {
            Debug.LogError("BoxLidController not found on the lid object!");
        }
    }

    void OnMouseDown()
    {
        // 뚜껑이 열리기 전에는 클릭되지 않음
        if (lidController != null && !lidController.IsLidOpen())
        {
            Debug.Log("The lid is still closed. Button cannot be clicked.");
            return; // 클릭 방지
        }

        // 오브젝트 클릭 시 상태 전환
        isOpen = !isOpen;
    }

    void Update()
    {
        // 문 회전 애니메이션
        if (isOpen)
        {
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, closedRotation, Time.deltaTime * speed);
        }
    }
}
