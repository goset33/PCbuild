using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketRing : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private int _progress = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball") )
        {
            _progress++;
            textMeshPro.text = _progress.ToString();
        }
    }
}
