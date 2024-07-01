using System;
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

    public Transform spawnPCTransform;
    public GameObject prefabPC;
    public GameObject spawningPC;
    public Material _matProcessor;
    public Color colorProcessor;

    private void Awake()
    {
        mailController.taskAccepted.AddListener(OnTaskAccepted);

        _matProcessor.mainTexture = new Texture2D(16, 16);
    }

    public void OnTaskAccepted(string[] i)
    {
        isTaskAccepted = true;
        requriedNames = i;
        spawningPC = Instantiate(prefabPC, spawnPCTransform.transform.position, spawnPCTransform.transform.rotation);
        buildPC = spawningPC.GetComponent<BuildPC>();
        spawningPC.SetActive(false);
        spawningPC.GetComponent<BuildPC>().onCompleteBuild.AddListener(colliders.Clear);
    }

    public void EnableBoxCollider()
    {
        GetComponent<Collider>().enabled = true;
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
            if (CheckColliders())
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
                for (int j = 0; j < 5; j++)
                {
                    buildPC.componets.Add(prefab);
                }
                List<PCComponets> hards = new List<PCComponets>();
                List<PCComponets> ram = new List<PCComponets>();

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
                        if(hards.Count < 2)
                        {
                            hards.Add(prefab);
                        }
                    }
                    else if (colliders[i].CompareTag("RAM"))
                    {
                        prefab.spawnObject = colliders[i].gameObject;
                        prefab.methodOnConnected = "IncrementProgress";
                        if (ram.Count < 4)
                        {
                            ram.Add(prefab);
                        }
                    }
                    
                    colliders[i].GetComponent<BuildComponentPC>().enabled = true;
                    Destroy(colliders[i].GetComponent<Rigidbody>());
                    Destroy(colliders[i].GetComponent<DragObject>());
                    colliders[i].gameObject.SetActive(false);
                    colliders[i].transform.SetParent(buildPC.startTransform);
                    colliders[i].transform.localPosition = Vector3.zero;
                    colliders[i].transform.localRotation = Quaternion.identity;
                    print(9);
                }
                buildPC.componets.AddRange(hards);
                buildPC.componets.AddRange(ram);
                buildPC.gameObject.SetActive(true);
                GetComponent<Collider>().enabled = false;
                buildPC.StartMinigame();
                buildPC.onEndMiniGame.AddListener(EnableBoxCollider);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
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

    private bool CheckColliders()
    {
        return GetElementsByTag("MotherBoard") > 0 &&
            GetElementsByTag("Core") > 0 &&
            GetElementsByTag("Cooler") > 0 &&
            GetElementsByTag("Power") > 0 &&
            GetElementsByTag("Videocard") > 0 &&
            GetElementsByTag("HardDrive") > 0 &&
            GetElementsByTag("RAM") > 0 &&
            GetElementsByTag("ComputerCase") > 0;

   }
    private int GetElementsByTag(string tag)
    {
        int i = 0;
        foreach(Collider c in colliders)
        {
            if(c.CompareTag(tag))
            {
                i++;
            }
        }
        return i;
    }
    
}
