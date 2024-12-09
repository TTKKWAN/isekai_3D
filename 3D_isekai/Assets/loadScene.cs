using UnityEngine;
using UnityEngine.SceneManagement; // 씬 로드를 위해 필요

public class LoadSceneOnClick : MonoBehaviour
{
    [Header("Scene Change Settings")]
    [SerializeField] private GameObject targetObject; // 클릭할 대상 오브젝트
    [SerializeField] private string targetSceneName = "3.Scene2"; // 로드할 씬 이름

    void Update()
    {
        // 마우스 클릭 이벤트 처리
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 특정 오브젝트를 클릭했는지 확인
                if (hit.transform.gameObject == targetObject)
                {
                    SceneManager.LoadScene("3. Scene2");
                }
            }
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Debug.Log($"씬 {targetSceneName}을(를) 로드합니다.");
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("로드할 씬 이름이 설정되지 않았습니다.");
        }
    }
}
