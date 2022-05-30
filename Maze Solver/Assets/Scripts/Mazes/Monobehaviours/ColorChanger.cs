using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void ChangeMaterial(Material material)
    {
        _renderer.material = material;
    }
    
    public void ChangeColor(Color color)
    {
        _renderer.materials[0].SetColor("_BaseColor", color);
    }
}
