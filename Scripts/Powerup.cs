﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = shields
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);
       if(other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                //enable triple shot
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    //enable speed boost here
                    player.SpeedBoostPowerupOn();
                }
                else if (powerupID == 2)
                {
                    //enable shields
                    player.EnableShields();
                }
            }

            
            //destroy ourself
            Destroy(this.gameObject);
       
        }
        
     
    }

}
