using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public MailController mailController;
    public BuildPC buildPC;

    public List<Collider> colliders;

    public bool isTaskAccepted;

    private string[] requriedNames;
    public int counter;

    private void Awake()
    {
        mailController.taskAccepted.AddListener(OnTaskAccepted);
    }

    public void OnTaskAccepted(string[] i)
    {
        isTaskAccepted = true;
        requriedNames = i;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (isTaskAccepted)
        {
            foreach (string requriedName in requriedNames)
            {
                other.gameObject.name = other.gameObject.name.Replace("(Clone)", "");
                if (other.gameObject.name.Equals(requriedName))
                {
                    colliders.Add(other);
                    counter++;
                }
            }
            if (counter == 8)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("ComputerCase"))
                    {
                        colliders.Remove(collider);
                        Destroy(collider.gameObject);
                        break;
                    }
                }

                buildPC.componets.Clear();
                PCComponets prefab = new PCComponets();
                for (int j = 0; j < 8; j++)
                {
                    buildPC.componets.Add(prefab);
                }

                for (int i = 0; i < colliders.Count; i++)
                {
                    if (colliders[i].CompareTag("MotherBoard"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "IncrementProgress";
                        buildPC.componets[0] = prefab;
                    }
                    else if (colliders[i].CompareTag("Core"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "StartProcessorMiniGame";
                        buildPC.componets[1] = prefab;
                    }
                    else if (colliders[i].CompareTag("Cooler"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "IncrementProgress";
                        buildPC.componets[2] = prefab;
                    }
                    else if (colliders[i].CompareTag("Power"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "StartWiresMiniGame";
                        buildPC.componets[3] = prefab;
                    }
                    else if (colliders[i].CompareTag("Videocard"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "IncrementProgress";
                        buildPC.componets[4] = prefab;
                    }
                    else if (colliders[i].CompareTag("HardDrive"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "IncrementProgress";
                        buildPC.componets[5] = prefab;
                    }
                    else if (colliders[i].CompareTag("RAM"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "StartFlashMinigame";
                        buildPC.componets[6] = prefab;
                    }
                    colliders[i].GetComponent<Rigidbody>().isKinematic = true;
                    colliders[i].GetComponent<DragObject>().enabled = false;
                    colliders[i].GetComponent<BuildComponentPC>().enabled = true;
                    colliders[i].gameObject.SetActive(false);
                    print(9);
                }
                buildPC.gameObject.SetActive(true);
                buildPC.StartMinigame();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (string requriedName in requriedNames)
        {
            if (other.gameObject.name == requriedName)
            {
                colliders.Remove(other);
                counter--;
            }
        }
    }
}
