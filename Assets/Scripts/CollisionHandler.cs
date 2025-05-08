using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float loadingDelay = 2f;
    [SerializeField]AudioClip explosionSFX;
    [SerializeField]AudioClip WinningSFX;

    
    AudioSource audioSource;
    int currentSceneIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Pickup":
                // Get Fuel System ready
                break;
            case "Landing":
                StartWinSwquence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(explosionSFX);
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", loadingDelay);
    }
    void StartWinSwquence()
    {
        audioSource.PlayOneShot(WinningSFX);
        GetComponent<Movement>().enabled = false;
        Invoke ("LoadNextLevel", loadingDelay);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
    
