using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    protected override void Feed()
    {
        Debug.Log("Dog is eating.");
        base.Feed();
    }
}
