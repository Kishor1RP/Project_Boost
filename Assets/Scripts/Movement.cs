using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainTrust = 1000f;
    [SerializeField]float rotaionThrust = 200f;
    [SerializeField]AudioClip mainThrustSFX;

    
    Rigidbody rb;
    AudioSource audiosource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(mainThrustSFX);
            }
            rb.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
        }
        else
        {
            audiosource.Stop();
        }
        
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotaionThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotaionThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
