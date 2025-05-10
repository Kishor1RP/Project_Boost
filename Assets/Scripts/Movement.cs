using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainTrust = 1000f;
    [SerializeField]float rotaionThrust = 200f;
    [SerializeField]AudioClip mainThrustSFX;
    [SerializeField]ParticleSystem mainThrustParticle;
    [SerializeField]ParticleSystem leftThrustParticle;
    [SerializeField]ParticleSystem rightThrustParticle;

    
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            StartThrustingLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StartThrustingRight();
        }
        else
        {
            StopSideThrusting();
        }
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
        PlayThrustSound();
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }
    void StopThrusting()
    {
        audiosource.Stop();
        mainThrustParticle.Stop();
    }
    void StartThrustingRight()
    {
        ApplyRotation(-rotaionThrust);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }
    void StartThrustingLeft()
    {
        ApplyRotation(rotaionThrust);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }
    void StopSideThrusting()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }
    void PlayThrustSound()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainThrustSFX);
        }
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
