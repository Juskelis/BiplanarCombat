using UnityEngine;
using System.Collections;

public class Bullet : Missile {
    public override void SetTarget(CustomCharacterController target)
    {
        this.target = null;
    }
}
