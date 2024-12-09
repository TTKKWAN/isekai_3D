using UnityEngine;

public class DragObject : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform player; // 플레이어 Transform
    [SerializeField] private Transform carryPosition; // 오브젝트를 들고 다닐 위치

    [Header("Allowed Objects to Carry")]
    [SerializeField] private GameObject[] allowedObjects; // 들고 다닐 수 있는 오브젝트 배열
    private GameObject pickedObject = null; // 현재 들고 있는 오브젝트

    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 2f; // 클릭 가능한 거리

    void Update()
    {
        // 마우스 클릭 감지 (오브젝트 집기)
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭
        {
            if (pickedObject == null)
            {
                TryPickObject(); // 오브젝트 집기
            }
        }

        // 'E' 키로 오브젝트 놓기
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject(); // 오브젝트 놓기
        }
    }

    private void TryPickObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            GameObject targetObject = hit.collider.gameObject;

            // 들고 다닐 수 있는 오브젝트인지 확인
            if (IsAllowedObject(targetObject))
            {
                pickedObject = targetObject;

                // 오브젝트를 플레이어의 carryPosition으로 이동
                pickedObject.transform.SetParent(carryPosition);
                pickedObject.transform.localPosition = Vector3.zero; // 위치 정렬
                pickedObject.transform.localRotation = Quaternion.identity; // 회전 정렬
                pickedObject.GetComponent<Rigidbody>().isKinematic = true; // 물리 효과 비활성화
                Debug.Log($"{pickedObject.name}을(를) 들었습니다.");
            }
            else
            {
                // Debug.Log($"{targetObject.name}은(는) 들 수 없는 오브젝트입니다.");
            }
        }
        else
        {

        }
    }


    private void DropObject()
    {
        if (pickedObject != null)
        {
            // 부모 관계 해제
            pickedObject.transform.SetParent(null);

            // 플레이어 앞에 오브젝트 놓기
            pickedObject.transform.position = player.position + player.forward * 1.0f; // 플레이어 앞 1m 거리로 설정

            // Rigidbody 활성화
            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // 물리 효과 활성화
                rb.velocity = Vector3.zero; // 기존 속도 초기화
                rb.angularVelocity = Vector3.zero; // 기존 회전 속도 초기화
            }

            // Collider가 Raycast에 반응하도록 설정
            Collider col = pickedObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true; // Collider 활성화
                col.isTrigger = false; // Trigger 해제
            }

            Debug.Log($"{pickedObject.name}을(를) 플레이어 앞에 놓았습니다: {pickedObject.transform.position}");

            // 오브젝트 초기화
            pickedObject = null;
        }
    }




    private bool IsAllowedObject(GameObject targetObject)
    {
        foreach (var obj in allowedObjects)
        {
            if (obj == targetObject)
            {
                return true;
            }
        }

        // 놓은 오브젝트도 허용
        if (pickedObject == targetObject)
        {
            return true;
        }

        return false;
    }




}
