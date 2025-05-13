using Meta.WitAi;
using Meta.WitAi.Json;
using Oculus.Voice;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject Abdulmalik;
    public AppVoiceExperience Voicerecognizer;
    // public InputField spellInputField; // Assign in Inspector

    void Start()
    {
        Voicerecognizer.VoiceEvents.OnPartialResponse.AddListener(SetSaidSpell);
    }

    private void SetSaidSpell(WitResponseNode response)
    {
        if (response["intents"].Count > 0)
        {
            string SaidSpell = response["intents"][0]["name"].Value.ToLower();
            Abdulmalik.SetActive(true);
        }
    }
}
