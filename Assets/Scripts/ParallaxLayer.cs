using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxLayer : MonoBehaviour
{
    [SerializeField]
    private string _layerName;

    [SerializeField]
    private Vector2 _scale;

    private Camera _cam;

    public SpriteRenderer Renderer { get; private set; }

    public string LayerName { get => _layerName; }

    public Vector2 Offset { get; set; }

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _cam = Camera.main;

        Offset = _cam.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector2.Scale(_cam.transform.position, _scale) - Offset;
    }
}