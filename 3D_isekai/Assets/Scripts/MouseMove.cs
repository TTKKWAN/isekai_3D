using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float mouseSensitivity = 100f; // 마우스 감도
    public Transform playerBody; // 플레이어의 몸체 (Y축 회전을 위해 필요)

    private float xRotation = 0f; // 카메라의 상하 회전 값
    [SerializeField] private Player player;


    void Start()
    {
        // 마우스 커서를 화면 중앙에 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!player.isEnding) {
            // 마우스 입력값 받기
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 상하 회전 계산 (120도 제한)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -60f, 90f); // 상하 제한 (120도)

            // 카메라 회전 적용 (상하)
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // 플레이어 몸체 회전 적용 (좌우, 180도 제한 없음)
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
