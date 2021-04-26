using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTiles : MonoBehaviour
{
    [SerializeField] private Color color1 = Color.white;
    [SerializeField] private Color color2 = Color.white;
    [SerializeField] private int tilesPerRow = 6;
    [SerializeField] private float emissionIntensity = 0.3f;

    private List<Material> _tiles = new List<Material>();
    private Conductor _conductor;
    private bool _initialized;
    private bool _even;
    private float _factor;

    private void Start()
    {
        foreach (Transform tile in transform)
        {
            _tiles.Add(tile.gameObject.GetComponent<MeshRenderer>().material);
        }

        _factor = Mathf.Pow(2, emissionIntensity);
    }

    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(UpdateColors);
        }
    }

    private void UpdateColors()
    {
        _even = !_even;
        for (int i = 0; i < _tiles.Count; i++)
        {
            if (i % tilesPerRow == 0)
            {
                _even = !_even;
            }

            Color colorToSet = _even ? color1 : color2;
            Color emissionColor = new Color(colorToSet.r * _factor, colorToSet.g * _factor, colorToSet.b * _factor);

            _tiles[i].SetColor("_BaseColor", colorToSet);
            _tiles[i].SetColor("_EmissionColor", emissionColor);
            _even = !_even;
        }
    }
}