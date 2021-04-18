using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public CameraController MainCamera;
    public PlayerController Player;

    public Elevator[] elevators;

    public Image ElevatorPanelImage;

    public Text JumpText;
    public Text FloorText;
    public Text ElevatorText;
    public Text TimeText;
    public Text TipText;

    public void Go(int destinationFloor)
    {
        foreach(var elevator in elevators)
        {
            if (elevator.havePlayer)
            {
                if(elevator.CurrentFloor != destinationFloor)
                {
                    elevator.ChooseDestination(destinationFloor);
                    ElevatorPanelImage.gameObject.SetActive(false);
                    Player.Freeze = false;
                    MainCamera.Gameplay = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    public void Open()
    {
        foreach (var elevator in elevators)
        {
            if (!elevator.havePlayer)
                return;

            elevator.doors[elevator.CurrentFloor].Open();
            elevator.havePlayer = false;
            ElevatorPanelImage.gameObject.SetActive(false);
            Player.Freeze = false;
            MainCamera.Gameplay = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
