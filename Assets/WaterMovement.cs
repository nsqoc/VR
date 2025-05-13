using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public Transform bottle;  
    public Transform waterSurface;  

    public float smoothSpeed = 5f;  
    void Update()
    {
        
        Quaternion targetRotation = Quaternion.Inverse(bottle.rotation);
        waterSurface.rotation = Quaternion.Lerp(waterSurface.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
