using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private StageData[] stages;
    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private Button buttonPrefab;

    [ContextMenu("Create Buttons")]
    public void CreateButtons()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            int index = i;
            Button button = Instantiate(buttonPrefab, buttonParent);
            button.GetComponentInChildren<TMP_Text>().text = stages[index].stageName;
        }
    }


    public void SelectStage(int index)
    {
        Debug.Log($"Selected Stage: {stages[index].stageName}");
        GameManager.Instance.SetStage(stages[index]);
        SceneManager.LoadScene(stages[index].sceneName);
    }

}
