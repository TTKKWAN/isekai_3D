using UnityEngine;

public class ObjectSlidingOnClick : MonoBehaviour
{
    [Header("Sliding Settings")]
    [SerializeField] private GameObject[] slidingObjects; // 슬라이딩할 오브젝트 배열
    [SerializeField] private float slideDistance = 2f; // 슬라이딩 거리
    [SerializeField] private float slideSpeed = 2f; // 슬라이딩 속도
    [SerializeField] private GameObject Cabinet;

    private bool isSliding = false; // 슬라이딩 여부
    private Vector3[] slidingTargetPositions; // 각 오브젝트의 슬라이딩 목표 위치

    void Start()
    {
        if (slidingObjects != null && slidingObjects.Length > 0)
        {
            slidingTargetPositions = new Vector3[slidingObjects.Length];
        }
        else
        {
            Debug.LogError("슬라이딩할 오브젝트가 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        // 슬라이딩 중이라면 모든 오브젝트를 목표 위치로 이동
        if (isSliding)
        {
            bool allObjectsReached = true;

            for (int i = 0; i < slidingObjects.Length; i++)
            {
                
                if (slidingObjects[i] != null)
                {
                    slidingObjects[i].transform.position = Vector3.MoveTowards(
                        slidingObjects[i].transform.position,
                        slidingTargetPositions[i],
                        slideSpeed * Time.deltaTime
                    );
                    

                    // 목표 위치에 도달하지 않은 오브젝트가 있다면 false
                    if (Vector3.Distance(slidingObjects[i].transform.position, slidingTargetPositions[i]) >= 0.01f)
                    {
                        allObjectsReached = false;
                    }
                }
            }

            // 모든 오브젝트가 목표 위치에 도달하면 슬라이딩 종료
            if (allObjectsReached)
            {
                isSliding = false;
            }
        }

        // 마우스 클릭 이벤트 처리
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 스크립트가 붙어있는 오브젝트를 클릭했는지 확인
                if (hit.transform.gameObject == gameObject)
                {
                    AudioSource Cabinetsound = Cabinet.GetComponent<AudioSource>();
                    if (Cabinetsound != null) {
                        Cabinetsound.Play();
                    }
                    StartSliding();
                    Debug.Log($"{gameObject.name}을(를) 클릭했습니다. 모든 오브젝트가 슬라이딩합니다.");
                }
            }
        }
    }

    private void StartSliding()
    {
        if (slidingObjects != null && slidingObjects.Length > 0)
        {
            for (int i = 0; i < slidingObjects.Length; i++)
            {
                if (slidingObjects[i] != null)
                {
                    // 각 오브젝트의 목표 위치를 동일한 거리만큼 설정
                    slidingTargetPositions[i] = slidingObjects[i].transform.position + Vector3.right * slideDistance;
                }
            }
            isSliding = true;
        }
        else
        {
            Debug.LogError("슬라이딩할 오브젝트(slidingObjects)가 설정되지 않았습니다.");
        }
    }
}
