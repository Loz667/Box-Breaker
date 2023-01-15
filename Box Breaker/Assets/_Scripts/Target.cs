using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody m_rb;

    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawn = -6;

    public int pointValue;
    public ParticleSystem explosion;

    private void Start()
    {
        //Randomly tosses object into play area
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(RandomForce(), ForceMode.Impulse);
        m_rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        GameManager.Instance.UpdateScore(pointValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy (gameObject);
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
        return new Vector3(Random.Range(-xRange, xRange), ySpawn);
    }
}
