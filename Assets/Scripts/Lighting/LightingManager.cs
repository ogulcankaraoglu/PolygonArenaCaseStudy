using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Material InventorySkybox;

    private void Start()
    {
        // Set the gradient skybox for backgorund, no need to update GI since we only want the background visual.
        RenderSettings.skybox = InventorySkybox;
    }
}
