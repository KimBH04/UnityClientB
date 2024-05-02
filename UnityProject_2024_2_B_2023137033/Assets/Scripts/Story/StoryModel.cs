using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGame;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryTableObject", order = 0)]
public class StoryModel : ScriptableObject
{
    public int storyNumber;
    public Texture2D mainImage;
    public bool storyDone;

    public StoryType storyType;

    [TextArea(10, 10)]
    public string storyText;
    public Option[] options;

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;
        public EventCheck eventCheck;
    }

    [System.Serializable]
    public class EventCheck
    {
        public int checkValue;
        public EventType type;
        public Result[] successResult;
        public Result[] failedResult;

        public enum EventType : int
        {
            None,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }
    }

    [System.Serializable]
    public class Result
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GoToEnding,
        }

        public int value;
        public Stats stats;
        public ResultType type;
    }

    public enum StoryType
    {
        Main,
        Sub,
        Serial
    }
}
