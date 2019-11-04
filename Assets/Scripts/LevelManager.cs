using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Rigidbody _rb;
    
    private float speed = 10;
    private float tumble = 10;
   
    
    Vector3 direction = new Vector3(0f,0f,-1f);
        
    public bool boost; 
    
    
    
    // Start is called before the first frame update
    void Start()
    {
      
        
        _rb = GetComponent<Rigidbody>();
        AsteroidRotation();
        RandomTrajectory();
       
        
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedIncrease();
        BoostTest();
        
    }

    private void SpeedIncrease()
    {
        if (boost)
        {
            speed = 30;
        }
        else
        {
            speed = 10;
        }

        _rb.velocity = direction * speed;

    }

    private void RandomTrajectory()
    {
        float randomNum = Random.Range(-.3f,.3f );
        
        direction += new Vector3(randomNum,randomNum,0f);

        direction = direction.normalized;
    }

    private void AsteroidRotation()
    {
        _rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void BoostTest()
    {
        if (Input.GetMouseButton(0))
        {
            boost = true;
        }
        else
        {
            boost = false;
        }
       
        
    }
}
