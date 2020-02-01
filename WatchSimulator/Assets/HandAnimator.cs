using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandAnimator : MonoBehaviour
{
    public VRTK.VRTK_InteractGrab grab;
    public Animator animator;
    string fistBool = "MakingFist";
    string daintBool = "HoldingDaintily";

    private void OnEnable() {
        grab.ControllerGrabInteractableObject += handleGrab;
        grab.ControllerUngrabInteractableObject += handleUngrab;
    }

    private void OnDisable() {
        grab.ControllerGrabInteractableObject -= handleGrab;
        grab.ControllerUngrabInteractableObject += handleUngrab;
    }

    void handleGrab(object sender, ObjectInteractEventArgs e) {
        animator.SetBool(daintBool, true);
    }

    void handleUngrab(object sender, ObjectInteractEventArgs e) {
        animator.SetBool(daintBool, false);
    }
}
