using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float sprintMultiplier = 2f; // 스프린트 시 속도 배수
    public float bobFrequency = 3f; // 흔들림 주기
    public float bobAmplitude = 0.1f; // 흔들림 크기
    public Transform cameraTransform; // 플레이어의 카메라

    public bool isKey2 = false; // 키 획득 여부
    public bool isPin = false;

    private CharacterController characterController;
    private float bobTimer = 0f;
    private Vector3 cameraInitialPosition;
    public bool isEnding = false;

    
    [SerializeField] private GameObject uiButton1; // UI 버튼 오브젝트
    [SerializeField] private GameObject uiButton2;

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


        if (ClickInit.isReturn) {
            transform.position = new Vector3((float) 736.22, (float) 2.53, 683);
        }
        isEnding = false;

    }

    void Update()
    {
        // 입력값 초기화
        float horizontal = 0f;
        float vertical = 0f;
        

        if (!isEnding) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isEnding = !isEnding;
                uiButton1.SetActive(true); // UI 버튼 숨기기
                uiButton2.SetActive(true);
                
            }

            if (Input.GetKey(KeyCode.W)) vertical += 1f; // W 키
            if (Input.GetKey(KeyCode.S)) vertical -= 1f; // S 키
            if (Input.GetKey(KeyCode.A)) horizontal -= 1f; // A 키
            if (Input.GetKey(KeyCode.D)) horizontal += 1f; // D 키

            // ESC 키로 마우스 잠금 해제
            
            float currentSpeed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= sprintMultiplier;
            }

            // 이동 방향 계산
            Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
            characterController.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);

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

        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isEnding = !isEnding;
                uiButton1.SetActive(false); // UI 버튼 숨기기
                uiButton2.SetActive(false);
                
            }
            cameraTransform.localPosition = cameraInitialPosition;
            if (!characterController.enabled)
            {
                characterController.enabled = true;
            }
        }

        

        
    }
    public void End()
    {
        isEnding = true; // 이동 비활성화
    }
}
