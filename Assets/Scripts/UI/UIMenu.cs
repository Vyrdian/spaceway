using System;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private bool _loadedThroughController = false;

    private void Start()
    {
        if (!_loadedThroughController)
            gameObject.SetActive(false);
    }

    public void LoadedThroughController() => _loadedThroughController = true;
}
