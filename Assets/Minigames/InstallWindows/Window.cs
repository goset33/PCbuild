using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public ProgressInstallWindows progressBar;
    public float addPercent = 0.1f;

    public void Complete()
    {
        //progressBar.windows.Remove(gameObject);
        progressBar.StartCoroutine("SpawnWindow");
        progressBar.AddProgress(addPercent);
        progressBar.activeWindows.Remove(gameObject);
        Destroy(gameObject);
        
    }
}
