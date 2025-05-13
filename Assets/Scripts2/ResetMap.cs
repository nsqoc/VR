using UnityEngine;

public class ResetMap : MonoBehaviour
{
    // المرجع لموقع اللاعب (عادةً الكاميرا في VR)
    public Transform playerTransform;

    // الموقع والاتجاه الأصلي للاعب
    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;

    // قائمة بالعناصر التي تريد إعادة تعيينها (اختياري)
    public GameObject[] objectsToReset;
    private Vector3[] originalObjectPositions;
    private Quaternion[] originalObjectRotations;

    void Start()
    {
        // حفظ الموقع والاتجاه الأصلي للاعب
        originalPlayerPosition = playerTransform.position;
        originalPlayerRotation = playerTransform.rotation;

        // حفظ المواقع والاتجاهات الأصلية للعناصر
        originalObjectPositions = new Vector3[objectsToReset.Length];
        originalObjectRotations = new Quaternion[objectsToReset.Length];

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            originalObjectPositions[i] = objectsToReset[i].transform.position;
            originalObjectRotations[i] = objectsToReset[i].transform.rotation;
        }
    }

    // دالة إعادة التعيين
    public void Reset()
    {
        // إعادة تعيين موقع واتجاه اللاعب
        playerTransform.position = originalPlayerPosition;
        playerTransform.rotation = originalPlayerRotation;

        // إعادة تعيين مواقع واتجاهات العناصر
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].transform.position = originalObjectPositions[i];
            objectsToReset[i].transform.rotation = originalObjectRotations[i];
        }

        Debug.Log("تم إعادة تعيين الخريطة.");
    }
}