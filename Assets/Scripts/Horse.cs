using UnityEngine;

public class Horse : Animal
{
    protected override void Feed()
    {
        Debug.Log("Horse is eating.");
        base.Feed();
    }
}
