using UnityEngine;
using TMPro;
using System.Xml.Serialization;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RecordTimeText : MonoBehaviour
{
    private TextMeshProUGUI recordTimeText;

    private void Awake()
    {
        recordTimeText = GetComponent<TextMeshProUGUI>();

        RecordManager.OnRecordUpdated += SetRecordText;
    }

    private void Start()
    {
        SetRecordText();
    }

    private void OnDestroy()
    {
        RecordManager.OnRecordUpdated -= SetRecordText;
    }

    private void SetRecordText()
    {
        recordTimeText.text = RecordManager.Instance.GetTimeRecordString();
    }
}
