using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGame
{
    public class Enums
    {
        public enum StoryType
        {
            Main,
            Sub,
            Serial,
        }

        public enum EventType
        {
            None,
            GoBattle = 100,
            CheckSTR = 1000,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA,
        }

        public enum ResultType
        {
            ChangeHP,
            ChangeSP,
            AddExperience = 100,
            GoToShop = 1000,
            GoToNextStory = 2000,
            GoToRandomStory = 3000,
            GoToEnding = 10000,
        }
    }

    [Serializable]
    public class Stats
    {
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        public int strength;
        public int dexterity;
        public int consitiution;
        public int intelligence;
        public int wisdom;
        public int charisma;
    }
}
