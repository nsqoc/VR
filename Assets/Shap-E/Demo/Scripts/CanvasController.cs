using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AiKodexShapE
{
    public class CanvasController : MonoBehaviour
    {
        public Button launcher;

        void Start()
        {
            launcher.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            #if UNITY_EDITOR
            EditorApplication.ExecuteMenuItem("Window/Shap-E");
            #else
            Debug.LogWarning("EditorApplication.ExecuteMenuItem لا يعمل خارج Unity Editor!");
            #endif
        }
    }
}
