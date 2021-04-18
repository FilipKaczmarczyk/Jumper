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

    public bool UseZone = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && UseZone)
        {
            if (Floor != Elevator.CurrentFloor)
            {
                if (!Elevator.IsMoving)
                {
                    Elevator.ChooseDestination(Floor);
                    SoundManager.PlaySFX(0);
                }
            }
            else
            {
                if (Door.IsOpen)
                {
                    Door.Close();
                }
                else
                {
                    Door.Open();
                }
            }

            UI.ElevatorText.text = "Current Floor:  " + Floor.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UseZone = true;
        }

        UI.TipText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UseZone = false;
        }

        UI.TipText.gameObject.SetActive(false);
    }
}
