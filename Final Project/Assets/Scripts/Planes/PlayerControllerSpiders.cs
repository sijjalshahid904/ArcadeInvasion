using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControllerSpiders : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    
    private StartGame sg;
    private BodyMove bm;
    private float mouseY;
    public float rotationSpeed = 100.0f;
    public float maxRange = 135.0f;
    public float minRange = 0.0f;
    public int lives = 5;

    public AudioClip collisionSoundClip; // Assign the audio clip in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the collision sound clip to the AudioSource component
        audioSource.clip = collisionSoundClip;
        sg = GameObject.Find("GameStart").GetComponent<StartGame>();
        bm = GameObject.Find("MoveS").GetComponent<BodyMove>();
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpiderFire"))
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            if (lives < 1)
            {
                sg.isLose = true;
                bm.isAlive = false;
                Destroy(gameObject);
            }
            else
            {
                lives -= 1;
                livesText.text = "Lives: " + lives;
            }
        }
    }
}
