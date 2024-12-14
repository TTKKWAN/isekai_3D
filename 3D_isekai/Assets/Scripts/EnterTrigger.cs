using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private Player player; // 플레이어 이동 스크립트
    [SerializeField] private GameObject uiButton1; // UI 버튼 오브젝트
    [SerializeField] private GameObject uiButton2;

    void Start()
    {
        if (uiButton1 != null && uiButton2 != null)
        {
            uiButton1.SetActive(false); // UI 버튼 숨기기
            uiButton2.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("엔딩 입장");
        if (other.CompareTag("Player")) // 플레이어와 충돌 감지
        {
            player.End(); // 이동 비활성화
            SceneManager.LoadScene("4. Ending");
        }
        ClickInit.isReturn = false;
    }
}
