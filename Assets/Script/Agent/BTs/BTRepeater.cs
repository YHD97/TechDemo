using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeater : Decorator
{
    public BTRepeater(BehaviourTree tree,Node nodes):base(tree,nodes){

    }

    public override BehaviourState Execute(){
        Debug.Log("Children returned"+Child.Execute());
        return BehaviourState.Running;
    }

    
}
