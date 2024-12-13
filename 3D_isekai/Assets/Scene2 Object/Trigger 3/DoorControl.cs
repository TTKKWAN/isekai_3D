using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator; // 서랍 애니메이터

    public void OpenDoor()
    {
        Debug.Log("문이 열립니다!");
        
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor"); // "Open" 트리거로 서랍 열기 애니메이션 실행
        }
    }
}
