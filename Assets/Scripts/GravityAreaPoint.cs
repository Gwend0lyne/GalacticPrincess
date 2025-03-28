using System.Collections.Generic;
using UnityEngine;

public class GravityAreaPoint : GravityArea
{
    public override Vector3 GetGravityDirection(GravityBody _gravityBody)
    {
        return (transform.position - _gravityBody.transform.position).normalized;
    }

}
