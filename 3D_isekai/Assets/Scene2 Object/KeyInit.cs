using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInit : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

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
        // 키 클릭 시 동작
        if (targetObject != null)
        {
            targetObject.SetActive(false); // 키 비활성화
        }

        // 플레이어의 isKey2 변수를 True로 설정
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.isKey2 = true;
        }

        Debug.Log("키가 비활성화되고 isKey2가 True로 설정되었습니다!");
    }
}
