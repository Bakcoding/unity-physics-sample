using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDebug : MonoBehaviour
{
    private static CanvasDebug instance;
    public static  CanvasDebug Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<CanvasDebug>();
                if (instance == null)
                {
                    GameObject go = new GameObject("CanvasDebug");
                    go.AddComponent<CanvasDebug>();
                }
            }
            return instance;
        }
    }

    [SerializeField] GameObject logObject;
    [SerializeField] Transform content;
    [SerializeField] Button btnClear;

    private List<GameObject> logs;

    private void Start()
    {
        logs = new List<GameObject>();
        btnClear.onClick.AddListener(ClearLog);
    }

    public void ShowLog(string log)
    {
        GameObject go = Instantiate(logObject);
        go.transform.SetParent(content, false);

        go.GetComponent<TMPro.TextMeshProUGUI>().text = log;
        go.SetActive(true);

        logs.Add(go);
    }

    public void ClearLog()
    {
        foreach(var log in logs)
        {
            Destroy(log);
        }
    }
}
