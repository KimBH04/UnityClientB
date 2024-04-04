using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections;

namespace StoryGame
{
#if UNITY_EDITOR
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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentModels = FindStoryModel(2);
            StartCoroutine(ShowText());
        }

        public enum GameState
        {
            StoryShow,
            StoryEnd,
            EndMode,
        }

        public StoryTableObject[] storyModels;
        public StoryTableObject currentModels;

        private StoryTableObject FindStoryModel(int number)
        {
            StoryTableObject tempStoryModels = null;
            for (int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)
                {
                    tempStoryModels = storyModels[i];
                    break;
                }
            }
            return tempStoryModels;
        }

        private IEnumerator ShowText()
        {
            for (int i = 0; i  <= currentModels.storyText.Length; i++)
            {
                currentText = currentModels.storyText[..i];
                textComponent.text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(delay);
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStoryModels()
        {
            storyModels = new StoryTableObject[0];
        }
#endif
    }
}