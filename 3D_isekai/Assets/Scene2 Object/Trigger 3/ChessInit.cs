using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessInit : MonoBehaviour
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
}
