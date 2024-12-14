using UnityEngine;
using System.Collections.Generic;

public class MinusTriggerController : MonoBehaviour
{
    [Header("Target Objects to Show")]
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>(); // 활성화할 오브젝트 목록

    [Header("Calculator Reference")]
    [SerializeField] private GameObject calculatorObject; // 계산기 오브젝트

    [Header("Minus Button and Equals Button")]
    [SerializeField] private GameObject minusButton; // - 버튼
    [SerializeField] private GameObject equalsButton; // = 버튼

    private bool isMinusPressed = false; // - 버튼이 눌렸는지 여부

    void Start()
    {
        // 모든 Target Objects 초기 비활성화
        foreach (GameObject target in targetObjects)
        {
            if (target != null)
            {
                target.SetActive(false);

                // Rigidbody 초기 비활성화
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // 물리 계산 초기 비활성화
                }
            }
        }

        if (minusButton == null)
        {
            Debug.LogError("Minus Button이 설정되지 않았습니다.");
        }

        if (equalsButton == null)
        {
            Debug.LogError("Equals Button이 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        if (calculatorObject == null) return;

        // 마우스 클릭 이벤트 처리
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.gameObject == minusButton)
                {
                    SetMinusPressed();
                }

                if (hit.transform.gameObject == equalsButton)
                {
                    OnEqualsPressed();
                }
            }
        }
    }

    public void SetMinusPressed()
    {
        isMinusPressed = true; // - 버튼이 눌렸음을 설정
        Debug.Log("Minus Button이 클릭되었습니다. isMinusPressed가 true로 설정되었습니다.");
    }

    private void OnEqualsPressed()
    {
        if (!isMinusPressed)
        {
            Debug.Log("isMinusPressed가 false입니다. 계산기가 사라지지 않습니다.");
            return;
        }

        Debug.Log("- 연산 후 = 버튼이 눌림: 계산기 사라지고 오브젝트 활성화");

        // Calculator Object 비활성화
        if (calculatorObject != null)
        {
            calculatorObject.SetActive(false);
            Debug.Log("Calculator Object가 비활성화되었습니다.");
        }

        // 모든 Target Objects 활성화
        foreach (GameObject target in targetObjects)
        {
            if (target != null)
            {
                target.SetActive(true);
                Debug.Log($"{target.name}이(가) 활성화되었습니다.");

                // Rigidbody를 활성화하여 중력 작용
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    AudioSource rbsound = rb.GetComponent<AudioSource>();
                    if (rbsound != null) {
                        rbsound.PlayDelayed((float) 0.2);
                    }
                    rb.isKinematic = false; // 물리 계산 활성화
                    Debug.Log($"{target.name}에 중력이 작용하기 시작했습니다.");
                }
            }
        }

        // 상태 초기화
        isMinusPressed = false;
        Debug.Log("isMinusPressed가 false로 초기화되었습니다.");
    }
}
