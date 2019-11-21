using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//USAGE: PUT ON ALL ASTEROIDS
//INTENT: ASTEROID MOVEMENT, ROTATION, HEALTH
public class AsteroidManager : MonoBehaviour
{

   private BlastMovement _shot;
    
    private Rigidbody _rb;

    private MeshRenderer _mesh;
    //Prefabs
    [FormerlySerializedAs("_asteroidChunksPrefab")] 
    public GameObject asteroidChunksPrefab;

    //Death variables
    private float _health = 6;
    
    //Movement  and rotation variables
    private float _speed = .5f;
    private float _tumble = 10;
   
    //Asteroid direction
    Vector3 direction = new Vector3(0f,0f,-1f);

    public bool inbombRange; 
        
    
    
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        AsteroidRotation();
        RandomTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //_rb.velocity = direction * _speed;
        _rb.AddForce((direction * _speed));
        
    }

    private void RandomTrajectory()
    {
        float randomNumX = Random.Range(-.3f,.3f);
        float randomNumY = Random.Range(-.3f, .3f);
        
        direction += new Vector3(randomNumX,randomNumY,0f) ;

        direction = direction.normalized;
    }

    private void AsteroidRotation()
    {
        _rb.angularVelocity = Random.insideUnitSphere * _tumble;
    }

    //detects what laser type and damage amount 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blast"))
        {
            _shot = other.transform.GetComponent<BlastMovement>();
            _health -= _shot.damage;
            Explosion();
            //DamageCalculator();
        }
    }

  

    //Destroys object and instantiates 3 smaller objects
    private void Explosion()
    {
        if (_health <= 0)
        {
          //  _mesh.enabled = false;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(asteroidChunksPrefab, transform.position, Quaternion.identity);

                asteroidChunksPrefab.GetComponent<Rigidbody>().velocity = direction * 2;

            }
            Destroy(gameObject);

        }
       
    }
    

}
