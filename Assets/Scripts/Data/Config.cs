﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori
{
    using Game;
    using Data;

    /// Unlock letters
    public enum UNLOCK { U, N, L, O, C, K }

    // --- Class Declaration ------------------------------------------------------------------------
    public partial class Config : IllogicGate.Singleton<Config>
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        /// <summary> Server URL for API calls </summary>
        public const string ServerURL = "https://arkaid.duckdns.org/wildlife/";

        /// <summary> Directory within the server where the character files are </summary>
        public const string CharactersURL = ServerURL + "characters/";

        /// <summary> API URL </summary>
        public const string APIURL = ServerURL + "api/";

        /// <summary> Color to paint the player, according to skill </summary>
        public static readonly Dictionary<Skill.Type, Color> SkillColor = new Dictionary<Skill.Type, Color>()
        {
            { Skill.Type.Shield, new Color32(22, 216, 40, 255) },
            { Skill.Type.Speed, new Color32(255, 217, 4, 255) },
            { Skill.Type.Freeze, new Color32(79, 211, 255, 255) },
        };

        /// <summary> Number of rounds to play </summary>
        public const int Rounds = 4;
        
        /// <summary> Levels of overall game difficulty </summary>
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard,
        }

        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------

        // --- Properties -------------------------------------------------------------------------------
        /// <summary> JSON object with fixed settings data. The user cannot change these </summary>
        JSONObject settings;

        /// <summary> shortcut to the JSON branch for the current difficulty </summary>
        JSONObject json { get { return settings[difficulty.ToString()]; } }
        
        /// <summary> Game difficulty </summary>
        public Difficulty difficulty {  get { return Options.instance.difficulty; } }
        
        /// <summary> Color for the active skill </summary>
        public Color skillColor { get { return SkillColor[Options.instance.skill]; } }

        
        /// <summary> Time for one round, adjusted for difficulty </summary>
        public int roundTime { get { return (int)json["round_time"].i; } }

        /// <summary> Needed clear ratio to win, adjusted for difficulty </summary>
        public float clearRatio { get { return json["clear_ratio"].f; } }

        /// <summary> Needed clear percentage to win, adjusted for difficulty </summary>
        public int clearPercentage { get { return Mathf.FloorToInt(clearRatio * 100); } }

        /// <summary> Speed multiplier for the speed skill, adjusted for difficulty </summary>
        public float speedSkillMultiplier { get { return json["skill_speed_multiplier"].f; } }

        /// <summary> Bonus time score, in bonus points per second remaining </summary>
        public float bonusTimeScore { get { return json["bonus_time_score"].f; } }

        /// <summary> Score for capturing a letter you already had </summary>
        public int unlockLetterScore { get { return (int)json["unlock_letter_score"].i; } }

        /// <summary> Starting amount of lives, adjusted for difficulty </summary>
        public int startLives { get { return (int)json["start_lives"].i; } }

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        protected override void OnInstanceCreated()
        {
            settings = new JSONObject(Resources.Load<TextAsset>("settings").text);
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Return enemy settings JSON for current difficulty
        /// </summary>
        public JSONObject GetEnemySettings(string enemyClassName)
        {
            JSONObject enemies = json["enemies"];

            if (enemies.HasField(enemyClassName))
                return enemies[enemyClassName];
            else
                return null;
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Returns how much score to assign per 1%, depending on how much you cleared
        /// </summary>
        public float CalculatePerPercentage(float clearedPercentage)
        {
            List<JSONObject> list = json["score_table"].list;
            for (int i = 0; i < list.Count; i++)
            {
                // first entry in is percentage, second one is score
                List<JSONObject> item = list[i].list;
                if (item[0].f >= clearedPercentage)
                    return clearedPercentage * item[1].f;
            }
            throw new System.Exception("Unexpected error");
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Maximum skill time
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public float GetSkillTime(Game.Skill.Type skill)
        {
            return json["skills"][skill.ToString()]["max_time"].f;
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Calculate how much skill to charge according to percentage cleared in one move
        /// </summary>
        public float CalculateSkillCharge(Game.Skill.Type skill, float percentage)
        {
            return json["skills"][skill.ToString()]["time_charge_per_percentage"].f * percentage;
        }
    }
}