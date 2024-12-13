using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickInit : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // 비활성화할 오브젝트
    [SerializeField] private string targetSceneName = "3. Scene1"; // 이동할 씬 이름
    public static bool isReturn = false;

    void Start()
    {
        // 오브젝트를 비활성화
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (targetObject != null)
        {
            isReturn = true;
            SceneManager.LoadScene(targetSceneName); // 씬 이동
        }
    }
}
