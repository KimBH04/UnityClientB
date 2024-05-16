using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StoryGame
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(GameSystem), true)]
    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gs = (GameSystem)target;

            if (GUILayout.Button("Reset Story Models"))
            {
                gs.ResetStoryModels();
            }

            if (GUILayout.Button("Assing Text Component by Name"))
            {
                GameObject textObject = GameObject.Find("Story Text UI");
                if (textObject != null)
                {
                    if (textObject.TryGetComponent(out TextMeshProUGUI textComponent))
                    {
                        gs.textComponent = textComponent;
                    }
                }
            }
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance;

        public float delay = 0.1f;
        private string currentText = string.Empty;

        public TMP_Text text;
        public TextMeshProUGUI textComponent;

        public GameState currentState;
        public Stats stats;
        public StoryModel[] storyModels;
        public int currentStoryIndex = 0;

        //public StoryTableObject currentModels;

        private void Awake()
        {
            Instance = this;
        }

        //private void Start()
        //{
        //    currentModels = FindStoryModel(2);
        //    StartCoroutine(ShowText());
        //}

        public void ChangeState(GameState state)
        {
            currentState = state;

            if (currentState == GameState.StoryShow)
            {
                StoryShow(currentStoryIndex);
            }
        }

        public void StoryShow(int number)
        {
            StoryModel tempStoryModels = FindStoryModel(number);

            StorySystem.Instance.currentStoryModel = tempStoryModels;
            StorySystem.Instance.CoShowText();
        }

        public void ApplyChoice(StoryModel.Result result)
        {
            switch (result.type)
            {
                case StoryModel.Result.ResultType.ChangeHp:
                    ChangeStats(result);
                    break;

                case StoryModel.Result.ResultType.GoToNextStory:
                    currentStoryIndex = result.value;
                    ChangeState(GameState.StoryShow);
                    ChangeStats(result);
                    break;

                case StoryModel.Result.ResultType.GoToRandomStory:
                    RandomStory();
                    ChangeState(GameState.StoryShow);
                    ChangeStats(result);
                    break;

                default:
                    Debug.LogError("Unknown effect type");
                    break;
            }
        }

        public void ChangeStats(StoryModel.Result result)
        {
            stats.hpPoint += result.stats.hpPoint;
            stats.spPoint += result.stats.spPoint;

            stats.currentHpPoint += result.stats.currentHpPoint;
            stats.currentSpPoint += result.stats.currentSpPoint;
            stats.currentXpPoint += result.stats.currentXpPoint;

            stats.strength += result.stats.strength;
            stats.dexterity += result.stats.dexterity;
            stats.consitiution += result.stats.consitiution;
            stats.wisdom += result.stats.wisdom;
            stats.intelligence += result.stats.intelligence;
            stats.charisma += result.stats.charisma;
        }

        StoryModel RandomStory()
        {
            StoryModel tempStoryModel = null;
            List<StoryModel> storyModels = new List<StoryModel>();

            for (int i = 0; i < this.storyModels.Length; i++)
            {
                if (storyModels[i].storyType == StoryModel.StoryType.Main)
                {
                    storyModels.Add(this.storyModels[i]);
                }
            }

            tempStoryModel = storyModels[Random.Range(0, storyModels.Count)];
            currentStoryIndex = tempStoryModel.storyNumber;
            Debug.Log($"{nameof(currentStoryIndex)} : {currentStoryIndex}");

            return tempStoryModel;
        }

        private StoryModel FindStoryModel(int number)
        {
            StoryModel tempStoryModel = null;

            for (int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)
                {
                    tempStoryModel = storyModels[i];
                    break;
                }
            }
            return tempStoryModel;
        }

        //private StoryTableObject FindStoryModel(int number)
        //{
        //    StoryTableObject tempStoryModels = null;
        //    for (int i = 0; i < storyModels.Length; i++)
        //    {
        //        if (storyModels[i].storyNumber == number)
        //        {
        //            tempStoryModels = storyModels[i];
        //            break;
        //        }
        //    }
        //    return tempStoryModels;
        //}

        //private IEnumerator ShowText()
        //{
        //    for (int i = 0; i  <= currentModels.storyText.Length; i++)
        //    {
        //        currentText = currentModels.storyText[..i];
        //        textComponent.text = currentText;
        //        yield return new WaitForSeconds(delay);
        //    }
        //    yield return new WaitForSeconds(delay);
        //}

        public enum GameState
        {
            StoryShow,
            StoryEnd,
            EndMode,
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryModel>("");
        }
#endif
    }
}