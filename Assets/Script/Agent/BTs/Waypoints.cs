using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float speed;
    public List<Transform> waypoints;
    private Coroutine path;

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        path = StartCoroutine(PatroWaypoints());
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Vector3 displacement = Player.position -transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance (Player.position, transform.position) < 10.0f) {
            transform.position += (displacement * Time.deltaTime*speed);
                        
        }else{
            PatroWaypoints();
        }
        
    }
    public void StopPatrolling(){
        StopCoroutine(path);
    }
    protected IEnumerator PatroWaypoints(){
        while(true){
            foreach (Transform point in waypoints)
            {
                Debug.Log("path:"+point);
                while(transform.position!=point.position){
                    transform.position = Vector3.MoveTowards(transform.position,point.position,speed * Time.deltaTime);
                    yield return null;
                }
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}
