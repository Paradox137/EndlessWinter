﻿using System;
using GameModule.ServiceModule;
using UnityEngine;

namespace GameModule.SettingsModule
{
    [CreateAssetMenu(fileName = "CharsTimeDelaySettings", menuName = "MyAssets/Game/Settings/CharsTimeDelaySettings")]
    public class CharsTimeDelaySettings : ScriptableObject
    {
        [Header("NORMAL MODE")]
        public CharsTimeDelay NormalDelay;

        [Space(10f)] [Header("SPEED MODE")]
        public CharsTimeDelay SpeedDelay;


        public float GetSpaceDelay(WriteMode __mode) => __mode == WriteMode.NormalMode ? NormalDelay.SpaceDelay : SpeedDelay.SpaceDelay;
        public float GetDefaultDelay(WriteMode __mode) => __mode == WriteMode.NormalMode ? NormalDelay.DefaultDelay : SpeedDelay.DefaultDelay;
        public float GetSpecialDelay(WriteMode __mode) => __mode == WriteMode.NormalMode ? NormalDelay.SpecialDelay : SpeedDelay.SpecialDelay;
        public float GetEndDelay(WriteMode __mode) => __mode == WriteMode.NormalMode ? NormalDelay.EndDelay : SpeedDelay.EndDelay;
    }

    [Serializable]
    public class CharsTimeDelay
    {
        [Range(0f, 2000f)] public float SpaceDelay;
        [Range(0f, 2000f)] public float DefaultDelay;
        [Range(0f, 2000f)] public float SpecialDelay;
        [Range(0f, 2000f)] public float EndDelay;
    }
}

