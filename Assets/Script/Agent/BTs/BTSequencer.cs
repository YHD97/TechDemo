using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequencer : BTComposite
{
    private int currentNode = 0;
    public BTSequencer(BehaviourTree tree, Node[] children):base(tree,children){

    }
    public override BehaviourState Execute(){
        if(currentNode < Children.Count){
            BehaviourState state = Children[currentNode].Execute();

            if(state == BehaviourState.Running){
                return BehaviourState.Running;
            }else if(state == BehaviourState.Failure){
                currentNode = 0;
                return BehaviourState.Failure;
            }else{
                currentNode++;
                if(currentNode<Children.Count){
                    return BehaviourState.Running;
                }else{
                    currentNode = 0;
                    return BehaviourState.Success;
                }

            }
        }
        return BehaviourState.Success;
    }
}
