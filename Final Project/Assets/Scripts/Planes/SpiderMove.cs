using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    private GameObject player;
    public GameObject beamPrefab;
    public GameObject firePrefab;
    private StartGame sg;
    public float speed = 2.0f;
    private bool hasStopped = false;
    public Vector3 pos;
    private bool checkRotate = false;
    public int hits;
    public float scaleFactor = 0.1f;
    private bool isCastingBeam = false;
    private BodyMove bm;
    public bool right = false;
    public ParticleSystem ps;
    public ParticleSystem psPlayer;
    private bool shouldCastBeam = false;
    public AudioClip collisionSoundClip; // Assign the audio clip in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the collision sound clip to the AudioSource component
        audioSource.clip = collisionSoundClip;

        player = GameObject.FindGameObjectWithTag("Player");
        sg = GameObject.Find("GameStart").GetComponent<StartGame>();
        bm = GameObject.Find("MoveS").GetComponent<BodyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkRotate==false)
        {
            if (transform.position.y > pos.y)
            {
               
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            }
            else
            {
                // Stop the movement
                enabled = false;
                hasStopped = true;
            }
        }
        if (checkRotate == true)
        {
            if (transform.position.x > pos.x)
            {
              
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            }
            else
            {
                // Stop the movement
                enabled = false;
                hasStopped = true;
            }
        }

        if (hasStopped && !isCastingBeam && !shouldCastBeam)
        {
            shouldCastBeam = true;
            StartCoroutine(CastCheckCoroutine());
            transform.rotation = Quaternion.Euler(0, -90, 90);
        }

    }
    public void rotateIt()
    {
        transform.rotation = Quaternion.Euler(0, -90, 90);
        checkRotate = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        if (collision.gameObject.CompareTag("Player"))
        {
            sg.isLose = true;
            bm.isAlive = false;
            Instantiate(psPlayer, collision.transform.position, psPlayer.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            Destroy(collision.gameObject);
            if(hits < 1)
            {
                Instantiate(ps, transform.position, ps.transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = transform.localScale - new Vector3(0.005f, 0.005f, 0.005f);
                hits = hits - 1;
            }
        }
    }
    IEnumerator waiting()
    {
        yield return new WaitForSeconds(4);
    }
    IEnumerator CastCheckCoroutine()
    {
        while (shouldCastBeam)
        {
            isCastingBeam = true;
            CastBeam();
            yield return StartCoroutine(waiting());
            isCastingBeam = false;
        }
    }
    void CastBeam()
    {
        if (Random.Range(0, 2) == 1 && bm.isAlive == true)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float beamScale = distance * scaleFactor;
            GameObject beamObject = Instantiate(beamPrefab, transform.position, rotation);
            GameObject SpFire = Instantiate(firePrefab, transform.position, rotation);
            SpiderShoot spiderShoot = SpFire.GetComponent<SpiderShoot>();
            if(hits==0)
                spiderShoot.speed = 2f;
            else
                spiderShoot.speed = hits * 3f;
            beamObject.transform.localScale = new Vector3(0.05f, 0.05f, beamScale);
            beamObject.transform.position += direction * distance * 0.5f;
            beamObject.transform.localScale = new Vector3(0.05f, 0.05f, distance - 0.6f);
            Destroy(beamObject, 1f);
        }
    }

}
