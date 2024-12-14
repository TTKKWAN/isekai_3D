using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public Image crosshairImage; // Crosshair 이미지
    [SerializeField] private Player player;

    void Start()
    {
        // 게임 시작 시 기본적으로 활성화
        crosshairImage.enabled = true;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            crosshairImage.enabled = !crosshairImage.enabled; // 토글
        }
        
    }
}
