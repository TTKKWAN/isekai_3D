using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private Animator drawerAnimator; // 서랍 애니메이터

    public void OpenDrawer()
    {
        Debug.Log("서랍이 열립니다!");
        
        if (drawerAnimator != null)
        {
            drawerAnimator.SetTrigger("Open"); // "Open" 트리거로 서랍 열기 애니메이션 실행
        }
    }
}

