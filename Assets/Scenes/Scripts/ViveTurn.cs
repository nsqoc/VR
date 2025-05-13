using UnityEngine;
using UnityEngine.XR;

public class ViveTurn : MonoBehaviour
{
    public ParticleSystem system;
    private AudioSource audioSource;

    public bool isFireKnob = true;
    private float rotVal = 0;
    private float scale = 0.5f;

    public Vector3 turnAxes;
    private Vector3 storedRotation;

    public bool createDialogue;
    private int numTimesPickedUp = 0;
    public string firstTouchDialogue;

    public bool isTool;
    public bool isActive;

    public float soundThreshold = 0.5f;

    private bool isGrabbing = false;
    private InputDevice controller;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        HandleVRInput();
    }

    private void HandleVRInput()
    {
        if (controller.TryGetFeatureValue(CommonUsages.gripButton, out bool isGripping) && isGripping)
        {
            if (!isGrabbing)
            {
                onGrab();
                isGrabbing = true;
            }
        }
        else
        {
            if (isGrabbing)
            {
                onRelease();
                isGrabbing = false;
            }
        }

        if (isGrabbing)
        {
            if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 deltaRotation))
            {
                UpdateRotation(new Vector3(deltaRotation.x, deltaRotation.y, 0));
            }
        }
    }

    public void onHover()
    {
        // تمت إزالة استدعاء makeTransparent
    }

    public void onHoverLeave()
    {
        // تمت إزالة استدعاء makeOpaque
    }

    public void onGrab()
    {
        numTimesPickedUp++;
        // تمت إزالة استدعاء makeOpaque
        storedRotation = transform.eulerAngles;
    }

    public void UpdateRotation(Vector3 delta)
    {
        delta.x *= turnAxes.x;
        delta.y *= turnAxes.y;
        delta.z *= turnAxes.z;

        transform.eulerAngles += delta;

        float prevRotVal = rotVal;

        if (delta.x > 0 || delta.y > 0 || delta.z > 0)
        {
            rotVal += scale;
        }
        else if (delta.x < 0 || delta.y < 0 || delta.z < 0)
        {
            rotVal -= scale;
        }

        if (isFireKnob)
        {
            var main = system.main;
            main.startSpeed = Mathf.Clamp(rotVal, 0, 35f);
        }
        else
        {
            var emission = system.emission;
            emission.rateOverTime = Mathf.Clamp(rotVal, 0, 50f);
        }

        if (rotVal <= 0)
        {
            rotVal = 0;
            system.Stop();
            audioSource.Stop();
        }

        if (prevRotVal == 0 && rotVal != 0)
        {
            system.Play();
            audioSource.Play();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        // يمكنك إضافة تأثير صوتي عند الاصطدام هنا
    }

    public void onRelease()
    {
        isGrabbing = false;
    }
}