using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTest : GoapAction
{
    public override bool PrePerform()
    {
        return true;
    }
    
    public override bool PostPerform()
    {
        return true;
    }
}
