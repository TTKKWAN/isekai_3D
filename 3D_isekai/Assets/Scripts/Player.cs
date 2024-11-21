using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpHeight = 0.2f; // 점프 높이
    public float gravity = -9.81f; // 중력 값
    public float bobFrequency = 3f; // 흔들림 주기
    public float bobAmplitude = 0.1f; // 흔들림 크기
    public Transform cameraTransform; // 플레이어의 카메라

    private CharacterController characterController;
    private float bobTimer = 0f;
    private Vector3 cameraInitialPosition;
    private Vector3 velocity; // 현재 속도
    private bool isGrounded; // 땅에 닿았는지 여부
    private bool isCurseVisible;
    void Start()
    {
        // CharacterController 컴포넌트 가져오기
        characterController = GetComponent<CharacterController>();

        // 카메라의 초기 위치 저장
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        cameraInitialPosition = cameraTransform.localPosition;

        // 마우스 잠금 상태
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCurseVisible = false;
    }

    void Update()
    {
        // 땅에 닿았는지 확인
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 땅에 닿아 있을 때 속도를 초기화
        }

        // 입력값 초기화
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical += 1f; // W 키
        if (Input.GetKey(KeyCode.S)) vertical -= 1f; // S 키
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f; // A 키
        if (Input.GetKey(KeyCode.D)) horizontal += 1f; // D 키

        // 이동 방향 계산
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // Head Bobbing 처리
        if (moveDirection.magnitude > 0f) // 움직임이 있을 때만
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            cameraTransform.localPosition = cameraInitialPosition + new Vector3(0, bobOffset, 0);
        }
        else
        {
            // 움직임이 없을 때 카메라를 원래 위치로 복원
            bobTimer = 0f;
            cameraTransform.localPosition = cameraInitialPosition;
        }

        // 점프 처리
        if (Input.GetButtonDown("Jump") && isGrounded) // 기본 Jump 키: Space
        {
            velocity.y = jumpHeight * 10f; // 점프 순간 강한 속도 적용
        }

        // 중력 적용
        if (!isGrounded)
        {
            velocity.y += gravity * 2f * Time.deltaTime; // 낙하 속도 빠르게
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // 기본 중력
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // 최종 속도 적용
        characterController.Move(velocity * Time.deltaTime);

        // ESC 키로 마우스 잠금 해제
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCurseVisible == false) {
                Cursor.lockState = CursorLockMode.None; // 잠금 해제
                Cursor.visible = true;                 // 커서 표시
                isCurseVisible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isCurseVisible = false;
            }
            
        }
    }
}
