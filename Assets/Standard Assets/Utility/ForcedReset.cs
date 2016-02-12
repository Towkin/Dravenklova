using System;
using UnityEngine;
using UnityEngine.SceneManagement;
// We are not using the CrossPlatformInput package.
//using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GUITexture))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {

        // We are not using the CrossPlatformInput package.
        //if (CrossPlatformInputManager.GetButtonDown("ResetObject"))

        // if we have forced a reset ...
        if (Input.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).path);
        }
    }
}
