using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public MailController mailController;
    public BuildPC buildPC;

    public bool isTaskAccepted;

    private string[] requriedNames;
    private int counter;

    private void Awake()
    {
        mailController.taskAccepted.AddListener(OnTaskAccepted);
    }

    public void OnTaskAccepted(string[] i)
    {
        isTaskAccepted = true;
        requriedNames = i;
    }

    void Update()
    {
        if (isTaskAccepted)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity);
            foreach (Collider collider in colliders)
            {
                foreach (string requriedName in requriedNames)
                {
                    if (collider.gameObject.name == requriedName)
                    {
                        counter++;
                    }
                }
            }
            if (counter == 7)
            {
                foreach (Collider collider in colliders)
                {
                    for (int i = 0; i < buildPC.componets.Count; i++)
                    {
                        PCComponets prefab = buildPC.componets[i];
                        prefab.spawnObject = collider.gameObject;

                        buildPC.componets.Add(prefab);
                        collider.gameObject.SetActive(false);               
                    }
                }
            }
        }
    }
}
