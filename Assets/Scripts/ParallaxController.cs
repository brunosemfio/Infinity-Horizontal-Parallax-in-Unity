using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Camera _cam;

    private Vector2 _screenBounds;

    private Dictionary<string, List<ParallaxLayer>> _layers;

    private void Start()
    {
        _cam = Camera.main;

        _screenBounds = _cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _layers = new Dictionary<string, List<ParallaxLayer>>();

        foreach (ParallaxLayer layer in FindObjectsOfType<ParallaxLayer>())
            PrepareLayers(layer);
    }

    private void LateUpdate()
    {
        foreach (var layer in _layers)
        {
            var list = layer.Value;

            var bounds = list[0].Renderer.bounds;

            if (_cam.WorldToViewportPoint(list[0].transform.position - bounds.extents).x > -0.1)
            {
                Vector2 offset = list[0].Offset;
                offset.x += bounds.size.x;

                var last = list.GetAndRemove(list.Count - 1);
                last.Offset = offset;

                list.Insert(0, last);
            }

            if (_cam.WorldToViewportPoint(list.Last().transform.position + bounds.extents).x < 1.1)
            {
                Vector2 offset = list.Last().Offset;
                offset.x -= bounds.size.x;

                var first = list.GetAndRemove(0);
                first.Offset = offset;

                list.Add(first);
            }
        }
    }

    private void PrepareLayers(ParallaxLayer layer)
    {
        var layerWidth = layer.Renderer.bounds.size.x;

        var needed = Mathf.CeilToInt(_screenBounds.x * 2 / layerWidth);

        if (needed < 2) needed = 2;

        var position = transform.position;
        position.x = layerWidth * needed / -2f;

        layer.transform.position = position;

        var list = new List<ParallaxLayer> { layer };

        for (int i = 1; i <= needed; i++)
        {
            position.x += layerWidth;

            var obj = Instantiate(layer.gameObject);
            obj.transform.position = position;

            list.Add(obj.GetComponent<ParallaxLayer>());
        }

        _layers.Add(layer.LayerName, list);
    }
}