using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float loadingDelay = 2f;
    [SerializeField]AudioClip explosionSFX;
    [SerializeField]AudioClip winningSFX;
    [SerializeField]ParticleSystem explosionParticles;
    [SerializeField]ParticleSystem WinningPaticles;
    
    AudioSource audioSource;
    int currentSceneIndex;
    
    bool isTransioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        Cheats();
    }
    void OnCollisionEnter(Collision other) 
    {   
        if (isTransioning || collisionDisable){return;}
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
        isTransioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionSFX);
        explosionParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", loadingDelay);
    }
    void StartWinSwquence()
    {
        isTransioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(winningSFX);
        WinningPaticles.Play();
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
    void Cheats()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
            Debug.Log("Collisions Toggled");
        }
    }
}
    
