using UnityEngine;
using Oculus.Haptics;

public class haptic : MonoBehaviour
{
    public HapticClip hapticClip;
    private HapticClipPlayer hapticClipPlayer;
    public OVRInput.Button button;

    void Start()
    {
        hapticClipPlayer = new HapticClipPlayer(hapticClip);
    }

    void Update()
    {
        if (OVRInput.Get(button))
        {
            hapticClipPlayer.Play(Oculus.Haptics.Controller.Right);
        }
    }
}
