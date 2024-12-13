using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject PinObject;
    private void OnMouseDown()
    {
                // 키 클릭 시 동작
        if (PinObject != null)
        {
            PinObject.SetActive(false); // 키 비활성화
        }

        // 플레이어의 isKey2 변수를 True로 설정
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.isPin = true;
        }
    }
}
