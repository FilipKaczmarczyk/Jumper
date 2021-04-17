using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public SoundManager SoundManager;

    public UIController UI;

    public Elevator Elevator;
    public Door Door;
    public int Floor;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if(Floor != Elevator.CurrentFloor)
            {
                if (!Elevator.IsMoving)
                {
                    Elevator.ChooseDestination(Floor);
                    SoundManager.PlaySFX(0);
                }   
            }
            else
            {
                // TO DO
                Door.Open();
            }

            UI.ElevatorText.text = "Current Floor:  " + Floor.ToString();
        }
    }
}
