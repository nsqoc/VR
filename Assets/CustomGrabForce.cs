using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabForce : XRGrabInteractable
{
    public float grabForce = 50f;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // Start applying force toward the interactor
        Transform interactorTransform = args.interactor.transform;
        StartCoroutine(ApplyGrabForce(interactorTransform));
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // Stop applying force
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator ApplyGrabForce(Transform interactor)
    {
        while (interactor != null)
        {
            Vector3 direction = interactor.position - transform.position;
            rb.AddForce(direction.normalized * grabForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }
}
