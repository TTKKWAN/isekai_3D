using UnityEngine;

public class SlideOnClick : MonoBehaviour
{
    public float slideDistance = 2f; // 오른쪽으로 이동할 거리
    public float speed = 2f; // 이동 속도
    public Light targetLight; // 슬라이딩 중 켜질 Light

    private bool isSliding = false; // 슬라이딩 상태 확인
    private Vector3 startPosition; // 초기 위치
    private Vector3 targetPosition; // 이동 목표 위치
    [SerializeField] private GameObject secret;

    void Start()
    {
        // 초기 위치 설정
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 0, -slideDistance); // 오른쪽으로 이동

        // Light 초기 상태 설정 (꺼진 상태)
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }
    }

    void OnMouseDown()
    {
        // 클릭하면 슬라이딩 시작
        if (!isSliding)
        {
            isSliding = true;

        }
        AudioSource secretsound = secret.GetComponent<AudioSource>();
        if (secretsound != null) {
            secretsound.Play();
        }
    }

    void Update()
    {
        // 슬라이딩 동작
        if (isSliding)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            // 목표 위치에 거의 도달하면 슬라이딩 종료
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // 정확히 목표 위치로 설정
                isSliding = false;
            }
        }
    }
}
