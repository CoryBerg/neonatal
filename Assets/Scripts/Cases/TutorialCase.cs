using UnityEngine;
using System.Collections;

public class TutorialCase : RespiratoryCase {

    protected override void Awake() {
        base.Awake();
        decompTimer = 60000f;
        deathTimer = 900000f;
        babyBreath.both = true;
    }
}
