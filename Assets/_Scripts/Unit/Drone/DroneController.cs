using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : UnitFollowerBase
{
    private void Start()
    {
        baseOffset = new Vector3(3.2f, 3.2f, 1.0f);
    }

    protected override void Update()
    {
        base.Update();
    }
}
