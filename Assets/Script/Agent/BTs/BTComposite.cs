using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTComposite : Node
{
    public List<Node> Children{get;set;}

    public BTComposite(BehaviourTree tree,Node[] nodes):base(tree){
        Children = new List<Node>(nodes);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
