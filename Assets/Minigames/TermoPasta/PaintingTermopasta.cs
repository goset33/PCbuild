using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PaintingTermopasta : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float _brushSize = 8;
    [SerializeField] private int _textureSize = 24;

    [SerializeField] private Texture2D _texture;
    [SerializeField] private Material _material;
    [SerializeField] private Color _drawColor;

    public List<int> currentArray = new List<int>();

    [SerializeField] private int _completeDrawingPixelsCount;
    public UnityEvent OnCompleted;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    private void OnValidate()
    {
        if( _texture == null)
        {
            _texture = new Texture2D(_textureSize, _textureSize);
        }
        if(_texture.width != _textureSize)
        {
            _texture.Reinitialize(_textureSize, _textureSize);
        }
        _texture.wrapMode = TextureWrapMode.Repeat;
        _texture.filterMode = FilterMode.Point;

        _material.mainTexture = _texture;
        _texture.Apply();
        _completeDrawingPixelsCount = (int)Mathf.Pow(_textureSize, 2);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.CompareTag("Paint"))
                {
                    Debug.Log("123");
                    int rayX = (int)(hit.textureCoord.x * _textureSize);
                    int rayY = (int)(hit.textureCoord.y * _textureSize);

                    for (int y = 0; y < _brushSize; y++)
                    {
                        for (int x = 0; x < _brushSize; x++)
                        {
                            _texture.SetPixel(rayX + x , rayY + y, _drawColor);
                        }
                    }
                    _texture.Apply();
                }
            }
            for (int y = 0; y < _textureSize; y++)
            {
                for (int x = 0; x < _textureSize; x++)
                {
                    if (_texture.GetPixel(x, y) == _drawColor)
                    {

                        if (!currentArray.Contains(y * _textureSize + x))
                        {
                            currentArray.Add(y * _textureSize + x);
                        }

                    }
                }
            }

            if (currentArray.Count >= _completeDrawingPixelsCount / 1.5)
            {
                OnCompleted.Invoke();

            }
        }
        
        
    }
}
