using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressInstallWindows : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public GameObject windowGameObject;
    [SerializeField]
    private int _maxWindows = 4;
    public List<GameObject> allWindows = new List<GameObject>();
    public List<GameObject> activeWindows = new List<GameObject>();
    private int _targetWindows = 10;
    [SerializeField] private float _minRange = 2.0f;
    [SerializeField] private float _maxRange = 5.0f;

    public UnityEvent OnCompleted;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        slider.value = 0;
        text.text = Mathf.Floor(slider.value / slider.maxValue * 100).ToString() + " %";

        StartCoroutine("SpawnWindow");
    }

    public void AddProgress(float percent)
    {
        slider.value += percent;
        text.text = Mathf.Floor(slider.value / slider.maxValue * 100).ToString() + " %";
        if(text.text.Contains("100"))
        {
            OnCompleted.Invoke();
        }
    }

    public IEnumerator SpawnWindow()
    {
        if (allWindows.Count < _targetWindows)
        {
            GameObject window = Instantiate(windowGameObject, Vector3.zero, new Quaternion(), transform.parent);
            window.transform.SetParent(transform.parent);
            
            float x = Random.Range(_minRange * -1, _maxRange);
            float y = Random.Range(_minRange * -1, _maxRange);
            window.transform.position = new Vector3(0, 0, 0);
            window.transform.position = new Vector3(x + transform.parent.position.x, y + transform.parent.position.y, 0);
            allWindows.Add(window);
            activeWindows.Add(window);
            window.GetComponent<Window>().progressBar = this;
            yield return new WaitForSeconds(1);
            if (activeWindows.Count >= _maxWindows)
            {
                yield return null;
            }
            else
            {
                StartCoroutine("SpawnWindow");
                yield return null;
            }
        }
        yield return null;
    }
}
