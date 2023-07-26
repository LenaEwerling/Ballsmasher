using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int pointValue;
    private bool wasHighEnough = false;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasHighEnough && transform.position.y >= 1.0f)
        {
            wasHighEnough = true;
        }
        if (gameManager.isGameActive && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Vector3.Distance(worldPosition, transform.position) <= 0.3f)
            {
                Destroy(gameObject);
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(pointValue);
                if (gameObject.CompareTag("Bad"))
                {
                    gameManager.ResetScore();
                }
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

    Vector3 RandomSpawnPos()
    {
        return new Vector3 (Random.Range(-xRange, xRange), ySpawnPos);
    }   

    private void OnMouseDown() 
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.ResetScore();
            }
        }
    }

    // public void OnDrag(PointerEventData eventData) 
    // {
    //     Debug.Log("Drag");
    //     Vector3 mousePosition = eventData.position;
    //     mousePosition.z = -Camera.main.transform.position.z;
    //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //     if (gameManager.isGameActive && Vector3.Distance(worldPosition, transform.position) <= 0.1f)
    //     {
    //         Destroy(gameObject);
    //         Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //         gameManager.UpdateScore(pointValue);
    //         if (gameObject.CompareTag("Bad"))
    //         {
    //             gameManager.ResetScore();
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && wasHighEnough)
        {
            gameManager.UpdateLives(1);
        }
    }

}
