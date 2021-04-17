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

    public void Go0()
    {
        foreach(var elevator in elevators)
        {
            if (elevator.havePlayer)
            {
                if(elevator.CurrentFloor != 0)
                {
                    elevator.ChooseDestination(0);
                }
            }
        }
    }
    public void Go1()
    {
        foreach (var elevator in elevators)
        {
            if (elevator.havePlayer)
            {
                if (elevator.CurrentFloor != 1)
                {
                    elevator.ChooseDestination(1);
                }
            }
        }
    }

    public void Go2()
    {
        foreach (var elevator in elevators)
        {
            if (elevator.havePlayer)
            {
                if (elevator.CurrentFloor != 2)
                {
                    elevator.ChooseDestination(2);
                }
            }
        }
    }

    public void Open()
    {
        foreach (var elevator in elevators)
        {
            if (elevator.havePlayer)
            {
                elevator.doors[elevator.CurrentFloor].Open();
                ElevatorPanelImage.gameObject.SetActive(false);
                elevator.havePlayer = false;
                Player.Freeze = false;
                MainCamera.Gameplay = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
