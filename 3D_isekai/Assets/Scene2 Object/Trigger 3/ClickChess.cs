using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChess : MonoBehaviour
{
    [SerializeField] private GameObject Answer;
    [SerializeField] private DoorControl doorControl;
    [SerializeField] private GameObject Click;

    private void OnMouseDown()
    {
        if (Answer != null )
        {
            
            if (doorControl != null)
            {
                doorControl.OpenDoor();
            }

            if (Click != null) {
                Click.SetActive(true);
            }
        }
    }
}
