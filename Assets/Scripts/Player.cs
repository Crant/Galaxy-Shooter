using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;

    private float _canFire = -1f;
    private Spawn_Manager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL in Player.cs");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        float newYPos = transform.position.y;
        float newXPos = transform.position.x;

        // between -11.3 and 11.3 on X Axis
        float boundsXMax = 11.3f;
        float boundsXMin = -11.3f;
        if (transform.position.x >= boundsXMax)
        {
            transform.position = new Vector3(boundsXMin, transform.position.y, 0);
        }
        else if (transform.position.x <= boundsXMin)
        {
            transform.position = new Vector3(boundsXMax, transform.position.y, 0);
        }

        //below 0 and above -3.8 on Y Axis
        float boundsYMax = 0;
        float boundsYMin = -3.8f;
        if (transform.position.y >= boundsYMax)
        {
            transform.position = new Vector3(transform.position.x, boundsYMax, 0);
        }
        else if (transform.position.y <= boundsYMin)
        {
            transform.position = new Vector3(transform.position.x, boundsYMin, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
    }
    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
