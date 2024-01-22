using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 public class ErrorDetector : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI errorTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;

        HIde();
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if(type == LogType.Error || type == LogType.Exception)
        {
            Show();

            errorTextMesh.text = "Error: " + condition + "\n" + stackTrace;
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void HIde()
    {
        gameObject.SetActive(false);
    }
}
