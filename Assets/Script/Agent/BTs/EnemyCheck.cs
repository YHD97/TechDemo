using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform Player;
     
     void Start () {
         
     }
     

    void Update () {
        Player = GameObject.FindWithTag("Player").transform;
        Vector3 displacement = Player.position -transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance (Player.position, transform.position) < 20.0f) {
            transform.position += (displacement * speed * Time.deltaTime);
                        
        }else{
            Debug.Log("ssss");
        }
        //else{
        //                 //do whatever the enemy has to do with the player
        //         }
    }
}
