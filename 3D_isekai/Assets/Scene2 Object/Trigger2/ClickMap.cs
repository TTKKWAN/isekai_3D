using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMap : MonoBehaviour
{
    [SerializeField] private GameObject PinOnMap;
    [SerializeField] private GameObject Chess;

    private void OnMouseDown()
    {
        Player player = FindObjectOfType<Player>();

        if (PinOnMap != null && player != null && player.isPin)
        {
            PinOnMap.SetActive(true);
            Chess.SetActive(true);
        }
    }
}
