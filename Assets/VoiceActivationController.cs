using Meta.WitAi;
using Meta.WitAi.Requests;
using UnityEngine;
using UnityEngine.XR;
using Meta.WitAi.Configuration;

namespace Oculus.VoiceSDK.UX
{
    /// <summary>
    /// Activates voice input using a controller button press.
    /// </summary>
    public class VoiceActivationController : MonoBehaviour
    {
        /// <summary>
        /// Reference to the voice service that will be activated or deactivated
        /// </summary>
        [Tooltip("Reference to the current voice service")]
        [SerializeField] private VoiceService _voiceService;

        /// <summary>
        /// Whether to immediately send data to service or to wait for the audio threshold
        /// </summary>
        [Tooltip("Whether to immediately send data to service or to wait for the audio threshold")]
        [SerializeField] private bool _activateImmediately = false;

        /// <summary>
        /// Whether to immediately abort request activation on deactivate
        /// </summary>
        [Tooltip("Whether to immediately abort request activation on deactivate")]
        [SerializeField] private bool _deactivateAndAbort = false;

        /// <summary>
        /// Oculus button to trigger activation
        /// </summary>
        [Tooltip("Oculus controller button to activate/deactivate voice")]
        [SerializeField] private OVRInput.Button activationButton = OVRInput.Button.One; // A button by default

        private VoiceServiceRequest _request;
        private bool _isActive = false;

        private void Awake()
        {
            if (_voiceService == null)
            {
                _voiceService = FindObjectOfType<VoiceService>();
            }
        }

        private void OnEnable()
        {
            if (_voiceService != null)
            {
                _voiceService.VoiceEvents.OnStartListening.AddListener(OnStartListening);
                _voiceService.VoiceEvents.OnStoppedListening.AddListener(OnStopListening);
            }
        }

        private void OnDisable()
        {
            if (_voiceService != null)
            {
                _voiceService.VoiceEvents.OnStartListening.RemoveListener(OnStartListening);
                _voiceService.VoiceEvents.OnStoppedListening.RemoveListener(OnStopListening);
            }
        }

        private void Update()
        {
            // Check if the controller button is pressed
            if (OVRInput.GetDown(activationButton))
            {
                if (!_isActive)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
            }
        }

        private void Activate()
        {
            if (!_activateImmediately)
            {
                _request = _voiceService.Activate(new WitRequestOptions(), new VoiceServiceRequestEvents());
            }
            else
            {
                _request = _voiceService.ActivateImmediately(new WitRequestOptions(), new VoiceServiceRequestEvents());
            }
        }

        private void Deactivate()
        {
            if (!_deactivateAndAbort)
            {
                if (_request != null)
                {
                    _request.DeactivateAudio();
                }
                else
                {
                    _voiceService.Deactivate();
                }
            }
            else if (_request != null)
            {
                _request.Cancel();
            }
        }

        private void OnStartListening()
        {
            _isActive = true;
        }

        private void OnStopListening()
        {
            _isActive = false;
            _request = null;
        }
    }
}