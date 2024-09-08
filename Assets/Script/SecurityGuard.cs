using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : Agent
{
    SubGoal sg1 = new SubGoal("isWaitingForBathroom", 1, true);    
    SubGoal sg2 = new SubGoal("isWaitingForKitchen", 1, true);    
    SubGoal sg3 = new SubGoal("isWaitingForBunk", 1, true);    
    SubGoal sg4 = new SubGoal("isInBathroom", 1, true);    
    SubGoal sg5 = new SubGoal("isInKitchen", 1, true);    
    SubGoal sg6 = new SubGoal("isInBunkroom", 1, true);    


   
}
