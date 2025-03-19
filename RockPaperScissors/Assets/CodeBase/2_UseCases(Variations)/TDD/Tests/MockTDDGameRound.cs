using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockTDDGameRound : ITDDGameRound
{
    public OutCome LastOutCome { get; set; }

    public Sign? didCallEvaluatePlayerSignWith = null;
    public void EvaluatePlayerSign(Sign playerSign)
    {
        didCallEvaluatePlayerSignWith = playerSign;
    }
}
