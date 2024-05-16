using StoryGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    public static StorySystem Instance;

    public StoryModel currentStoryModel;

    public float delay = 0.1f;
    public string fullText;
    public string currentText = string.Empty;
    public Text textComponent;
    public Text storyIndex;
    public Image imageComponent;

    public Button[] buttonWay = new Button[3];
    public Text[] buttonWayText = new Text[3];

    private TextSystem textSystem = TextSystem.None;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < buttonWay.Length; i++)
        {
            int wayIndex = i;   //Ä¸Ã³ ¹æÁö
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));
        }

        StoryModelInit();
        StartCoroutine(ShowText());
    }

    public void CoShowText()
    {
        StoryModelInit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    public void ResetShow()
    {
        textComponent.text = string.Empty;

        for (int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }

    public void StoryModelInit()
    {
        fullText = currentStoryModel.storyText;
        storyIndex .text = currentStoryModel.storyNumber.ToString();

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;
        }
    }

    private IEnumerator ShowText()
    {
        textSystem = TextSystem.Doing;
        if (currentStoryModel.mainImage != null)
        {
            Rect rect = new(0f, 0f, currentStoryModel.mainImage.width, currentStoryModel.mainImage.height);
            Vector2 pivot = new(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(currentStoryModel.mainImage, rect, pivot);

            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogError("Cannot load image");
        }

        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText[..i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        textSystem = TextSystem.None;
    }

    public void OnWayClick(int index)
    {
        if (textSystem == TextSystem.Doing)
        {
            return;
        }

        Debug.Log(index);

        bool checkEventTypeNone = false;
        StoryModel playStoryMode = currentStoryModel;

        if (playStoryMode.options[index].eventCheck.type == StoryModel.EventCheck.EventType.None)
        {
            for (int i = 0; i < playStoryMode.options[index].eventCheck.successResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].eventCheck.successResult[i]);
                checkEventTypeNone = true;
            }
        }
    }

    public enum TextSystem
    {
        None,
        Doing,
        Select,
        Done
    }
}
