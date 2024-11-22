using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public Image crosshairImage; // Crosshair 이미지

    void Start()
    {
        // 게임 시작 시 기본적으로 활성화
        crosshairImage.enabled = true;
    }

    void Update()
    {
        // 예: ESC 키로 조준점 숨기기/표시
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            crosshairImage.enabled = !crosshairImage.enabled; // 토글
        }
    }
}
