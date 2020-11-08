using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum POWERUP_TYPE
    {
        TRIPLE_SHOT, SPEED, SHIELD
    };
    [SerializeField]
    private float _speed = 3f;
    // 0 = Triple shot, 1 = Speed, 2 = Shield
    [SerializeField]
    private int _powerup_ID = 0;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch(_powerup_ID)
                {
                    case 0://tripleshot
                        player.TripleShotActive();
                        break;
                    case 1://Speed
                        player.SpeedBoostActive();
                        break;
                    case 2://Shield
                        player.ShieldBoostActive();
                        break;
                    default:
                        Debug.Log("Wrong powerup ID was set");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
       
    }
}

