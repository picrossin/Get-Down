using System.Collections.Generic;
using UnityEngine;

public class DiscoTiles : MonoBehaviour
{
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private int tilesPerRow = 6;
    
    private List<Material> _tiles = new List<Material>();
    private Conductor _conductor;
    private bool _initialized;
    private bool _even;

    private void Start()
    {
        foreach (Transform tile in transform)
        {
            _tiles.Add(tile.gameObject.GetComponent<MeshRenderer>().material);
        }
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
            
            _tiles[i].SetColor("_BaseColor", _even ? color1 : color2);
            _even = !_even;
        }
    }
}
