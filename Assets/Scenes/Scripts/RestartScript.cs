using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus; // تأكد من إضافة Oculus Integration إلى المشروع

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        // إعادة تشغيل المشهد عند الضغط على زر A في يد Oculus اليمنى
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
