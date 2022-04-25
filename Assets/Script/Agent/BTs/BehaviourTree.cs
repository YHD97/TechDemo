using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    private Node mRoot;
    private bool startBehaviour;
    public Dictionary<string,object> BlackBoard{get;set;}
    public Node Root{get{return mRoot;}}
    private Coroutine behaviour;
    public Transform point1;
    public Transform point2;
    // Start is called before the first frame update
    void Start()
    {
        // Vector3 pointLD = new Vector3(point1.position.x,point1.position.y,point1.position.z);
        // Vector3 pointRU = new Vector3(point2.position.x,point2.position.y,point2.position.z);
        BlackBoard = new Dictionary<string,object>();
        BlackBoard.Add("world",new Rect(point1.position.x,point1.position.y,point2.position.x,point2.position.y));
        startBehaviour = false;

        mRoot = new BTRepeater(this,new BTSequencer(this,
        new Node[]{new BTRandomWalk(this)}));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!startBehaviour){
            behaviour = StartCoroutine(RunBehaviour());
            startBehaviour = true;
        }
        
    }

    private IEnumerator RunBehaviour(){
        Node.BehaviourState state = Root.Execute();
        while(state == Node.BehaviourState.Running){
            Debug.Log("Root state:"+ state);
            yield return null;
            state = Root.Execute();
        }
        Debug.Log("Root finished with:"+ state);
    }

    
}
