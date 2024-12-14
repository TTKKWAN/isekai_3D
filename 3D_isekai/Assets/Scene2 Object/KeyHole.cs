using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour
{
    [SerializeField] private DrawerController drawerController; // 서랍 애니메이터
    [SerializeField] private GameObject keyholeObject; // 열쇠구멍 오브젝트
    [SerializeField] private GameObject drawer;

    [SerializeField] private GameObject Map;

    private void OnMouseDown()
    {
        // Player 객체 찾기
        Player player = FindObjectOfType<Player>();

        if (player != null && player.isKey2) // isKey2가 True인지 확인
        {
            Debug.Log("열쇠 사용: 서랍 열림!");
            AudioSource drawAudio = drawer.GetComponent<AudioSource>();
            if (drawAudio != null)
            {
                drawAudio.Play();
            }
            // 열쇠구멍 오브젝트 비활성화
            if (keyholeObject != null)
            {
                keyholeObject.SetActive(false);
            }

            // 서랍 열기 애니메이션 실행
            if (drawerController != null)
            {
                drawerController.OpenDrawer();
            }

            if (Map != null) {
                Map.SetActive(true);
            }
        }
        else
        {
            Debug.Log("열쇠가 없습니다. 열쇠를 찾아야 합니다.");
        }
    }
}
