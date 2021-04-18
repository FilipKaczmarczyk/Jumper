using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPanel : MonoBehaviour
{
    public Platform Platform;

    public UIController UI;

    public int PanelNumber;

    public bool UseZone = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && UseZone)
        {
            Platform.IsCalled = true;
            Platform.CallingPanel = PanelNumber;
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
