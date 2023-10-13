using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPointSceneLoader : MonoBehaviour
{
    private void Awake()
    {
        if (EntryPoint.IsExist) return;

        SceneManager.LoadScene(0);
    }
}