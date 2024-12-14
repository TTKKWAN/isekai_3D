using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChess : MonoBehaviour
{
    [SerializeField] private GameObject Answer;
    [SerializeField] private DoorControl doorControl;
    [SerializeField] private GameObject Click;
    [SerializeField] private GameObject door;

    private void OnMouseDown()
    {
        if (Answer != null )
        {
            AudioSource doorAudio = door.GetComponent<AudioSource>();
            if (doorControl != null)
            {
                doorControl.OpenDoor();
            }
            if(doorAudio != null) {
                doorAudio.Play();
            }

            if (Click != null) {
                Click.SetActive(true);
            }
        }
    }
}
