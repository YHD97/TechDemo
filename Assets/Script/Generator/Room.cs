using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorUp,doorDown,doorLeft,doorRight;
    // Start is called before the first frame update
    public bool roomUp,roomDown,roomLeft,roomRight;
    public int stepToStart;
    public Text text;
    public int doorNumber;
    void Start()
    {
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRoom(float xOffsie,float yOffsize){
        stepToStart = (int)(Mathf.Abs(transform.position.x/xOffsie)+ Mathf.Abs(transform.position.y/yOffsize));
        text.text = stepToStart.ToString();

        if(roomUp){
            doorNumber++;
        }
        if(roomDown){
            doorNumber++;
        }
        if(roomLeft){
            doorNumber++;
        }
        if(roomRight){
            doorNumber++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            CameraController.instance.ChangeTarget(transform);
        }
    }
}
