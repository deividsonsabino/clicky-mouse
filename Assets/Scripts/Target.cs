using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetTb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = 2;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        targetTb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = gameManager.GetComponent<AudioSource>();

        targetTb.AddForce(RandomForce(), ForceMode.Impulse);
        targetTb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpwanPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            if(gameObject.CompareTag("Bad"))
            {
                playerAudio.PlayOneShot(gameManager.bombAudioClip,1.0f);
            }
            else
            {
                playerAudio.PlayOneShot(gameManager.attackAudioClip, 1.0f);
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            if(gameManager.lives == 0)
            {
                gameManager.GameOver();
                playerAudio.Stop();
                playerAudio.PlayOneShot(gameManager.gameOverAudioClip, 1.0f);
            }
            else
            {
                gameManager.UpdateLives();
            }
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpwanPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), -ySpawnPos);
    }
}
