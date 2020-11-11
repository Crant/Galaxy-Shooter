using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _deathAnim;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("_player is null in Enemy");
        }
        _deathAnim = gameObject.GetComponent<Animator>();
        if (_deathAnim == null)
        {
            Debug.LogError("_deathAnim is null in Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move down at 4 meters per second
        transform.Translate(Vector3.down * _speed  * Time.deltaTime);
        //if bottom at screen 
        //respawn at top with a new x position
        if(transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _deathAnim.SetTrigger("OnEnemyDeath");
            //Destroy enemy
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
       }
       else if(other.tag == "Laser")
       {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _deathAnim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            
            Destroy(this.gameObject, 2.8f);
        }
    }
}