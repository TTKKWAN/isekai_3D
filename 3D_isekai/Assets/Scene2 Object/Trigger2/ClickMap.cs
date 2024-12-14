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
        AudioSource PinAudio = PinOnMap.GetComponent<AudioSource>();

        if (PinOnMap != null && player != null && player.isPin && Chess != null)
        {
            PinOnMap.SetActive(true);
            Chess.SetActive(true);
            if (PinAudio != null)
            {
                PinAudio.Play();
            }

        }
    }
}
