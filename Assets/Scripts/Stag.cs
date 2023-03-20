using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stag : Animal
{
    protected override void Feed()
    {
        Debug.Log("Stag is eating.");
        base.Feed();
    }
}
