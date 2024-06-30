using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PCController : MonoBehaviour
{
    public GameObject box;
    public Transform boxSpawnPos;

    public Transform player;
    public GameObject monitorCam;

    [Header("Windows")]
    public GameObject MainWindow;
    public GameObject ShopWindow, MailWindow;

    private bool isPlayerInTrigger;
    private bool isInPC;
    public UnityEvent<string[]> isInMail;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Q))
        {
            OnComputerStart();
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

    public void OnComputerStart()
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

    public void OnMainButtonPressed(GameObject Window)
    {
        if (!Window.name.Equals("Main"))
        {
            MainWindow.SetActive(false);
            Window.SetActive(true);
            if (Window.name.Equals("Mail"))
            {
                isInMail.Invoke(null);
            }
        }
        else
        {
            ShopWindow.SetActive(false);
            MailWindow.SetActive(false);
            Window.SetActive(true);
        }
    }

    public IEnumerator Delivery(GameObject gameObject, int waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        GameObject newBox = Instantiate(box, boxSpawnPos.position, Quaternion.identity);
        newBox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        newBox.GetComponent<Rigidbody>().AddForce(-transform.forward * 17f, ForceMode.Impulse);
        newBox.GetComponent<OpenBoxer>().componentInBox = gameObject;
        yield return new WaitForSeconds(0.5f);
        newBox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
