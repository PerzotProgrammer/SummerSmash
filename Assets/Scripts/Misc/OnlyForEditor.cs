using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlyForEditor : MonoBehaviour
{
#if UNITY_EDITOR
    // !!! KOD TYLKO DO UŻYWANIA W EDYTORZE 
    // NIE WPŁYWA NA KOMPILACJE

    public void Start()
    {
        LoadUIIfNotLoaded();
    }

    private void LoadUIIfNotLoaded()
    {
        if (!SceneManager.GetSceneByName("Scenes/UI").isLoaded)
        {
            SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
        }
    }
#endif
}