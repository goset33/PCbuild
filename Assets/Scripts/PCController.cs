using System.Collections;
using UnityEngine;


public class PCController : MonoBehaviour
{
    public GameObject pcCanvas;
    public Transform player;
    public GameObject monitorCam;

    [Header("Windows")]
    public GameObject MainWindow;
    public GameObject ShopWindow, MailWindow;

    private bool isPlayerInTrigger;
    private bool isInPC;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            isInPC = !isInPC;
            if (isInPC)
            {
                player.GetChild(2).gameObject.SetActive(false);
                player.GetComponent<FirstPersonMovement>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                player.GetChild(0).gameObject.SetActive(false);
                monitorCam.SetActive(true);

                MainWindow.SetActive(true);
            }
            else
            {
                MainWindow.SetActive(false);
                ShopWindow.SetActive(false);
                MailWindow.SetActive(false);

                player.GetComponent<FirstPersonMovement>().enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                monitorCam.SetActive(false);
                player.GetChild(0).gameObject.SetActive(true);
                player.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Camera.main.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    public void OnButtonPressed(GameObject Window)
    {
        MainWindow.SetActive(false);
        Window.SetActive(true);
    }
}
