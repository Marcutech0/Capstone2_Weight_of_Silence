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
        yield return new WaitForSeconds(21f);
        SceneManager.LoadScene("Cutscene1.1");
    }
}
