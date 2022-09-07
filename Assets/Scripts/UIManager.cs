
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button rebuildButton;
    [SerializeField] private TMP_InputField rowColumnCount;
    [SerializeField] private TextMeshProUGUI matchCount;

    // Start is called before the first frame update
    void OnEnable()
    {
        rebuildButton.onClick.AddListener(RebuildButtonPressed);
        EventManager.OnMatchCounterChanged += EventManager_OnMatchCounterChanged;
    }

    private void OnDisable()
    {
        rebuildButton.onClick.RemoveListener(RebuildButtonPressed);
        EventManager.OnMatchCounterChanged -= EventManager_OnMatchCounterChanged;
    }


    private void EventManager_OnMatchCounterChanged(int count)
    {
        SetMatchCount(count);
    }

    void RebuildButtonPressed()
    {
        EventManager.GenerateGrid(int.Parse(rowColumnCount.text));
    }

    public void SetMatchCount(int count)
    {
        matchCount.text = "Match Count : " + count;
    }
      
}
