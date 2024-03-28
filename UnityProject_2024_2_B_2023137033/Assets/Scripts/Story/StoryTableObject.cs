using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGame
{
    [CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryTableObject", order = 0)]
    public class StoryTableObject : ScriptableObject
    {
        public int storyNumber;
        public Enums.StoryType storyType;
        public bool storyDone;

        [TextArea(10, 10)]
        public string storyText;
        public List<Option> options = new List<Option>();

        [Serializable]
        public class Option
        {
            public string optionText;
            public string buttonText;
            public EventCheck eventCheck;
        }

        [Serializable]
        public class EventCheck
        {
            public int checkValue;
            public Enums.EventType eventType;
            public List<Result> successResults = new List<Result>();
            public List<Result> failureResults = new List<Result>();
        }

        [Serializable]
        public class Result
        {
            public Enums.ResultType resultType;
            public int value;
            public Stats stats;
        }
    }

}