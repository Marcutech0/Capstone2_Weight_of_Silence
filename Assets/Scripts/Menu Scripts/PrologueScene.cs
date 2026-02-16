using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PrologueScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(43f);
        SceneManager.LoadScene("Exploration 1.1");
    }
}
