using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Material InventorySkybox;

    private void Start()
    {
        RenderSettings.skybox = InventorySkybox;
    }
}
