using UnityEngine;

public class ObjectClickDetector : MonoBehaviour
{
    public LayerMask clickableLayer; // Inspector에서 클릭 가능한 Layer 설정

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            DetectClickedObject();
        }
    }

    void DetectClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
        {
            Debug.Log($"Clicked on: {hit.transform.name}");
        }
        else
        {
            Debug.Log("Clicked on nothing!");
        }
    }
}
