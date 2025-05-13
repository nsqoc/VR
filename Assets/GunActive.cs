using Meta.WitAi;
using Meta.WitAi.Json;  // لإضافة دعم WitResponseNode
using Oculus.Voice;
using UnityEngine;
using UnityEngine.UI;

public class GunActive : MonoBehaviour
{
    public GameObject Abdullah;  // الكائن الذي سنتفاعل معه بناءً على الأوامر الصوتية
    public AppVoiceExperience Voicerecognizer;  // للتعرف على الصوت باستخدام Oculus/Meta Voice SDK
    // public InputField spellInputField;  // حقل إدخال النص في واجهة المستخدم إذا كان مستخدمًا

    void Start()
    {
        // ربط الحدث OnPartialResponse مع دالة لتشغيل التفاعل الصوتي
        Voicerecognizer.VoiceEvents.OnPartialResponse.AddListener(SetSaidSpell);
    }

    // الدالة التي تعالج استجابة الصوت
    private void SetSaidSpell(WitResponseNode response)
    {
        // التحقق إذا كانت الاستجابة تحتوي على نية
        if (response["intents"].Count > 0)
        {
            string SaidSpell = response["intents"][0]["name"].Value.ToLower();  // استخراج النية
            Abdullah.SetActive(true);  // تفعيل الكائن Abdullah عند التعرف على النية
        }
    }
}
