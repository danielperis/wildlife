﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class DataManager : IllogicGate.Singleton<DataManager>
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Properties -------------------------------------------------------------------------------
        /// <summary> Progress file. Edit with care! </summary>
        public JSONObject progress = new JSONObject();

        /// <summary> Location of the progress file </summary>
        string progressFilePath { get { return Application.persistentDataPath + "/progress.sav"; } }
        
        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        public void SaveProgress()
        {
            IllogicGate.Data.EncryptedFile.WriteJSONObject(progressFilePath, progress);
        }
        
        // -----------------------------------------------------------------------------------	
        public void LoadProgress()
        {
            progress = IllogicGate.Data.EncryptedFile.ReadJSONObject(progressFilePath);
        }
    }
}