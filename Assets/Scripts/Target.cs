using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetTb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = 2;

    // Start is called before the first frame update
    void Start()
    {
        targetTb = GetComponent<Rigidbody>();

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
        Destroy(gameObject);   
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
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
