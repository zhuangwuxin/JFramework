using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using JFramework.Editor;
#endif
using JFramework.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JFramework.Core
{
    public sealed partial class GlobalManager
    {
        [SerializeField, LabelText("控制台输出选项"), FoldoutGroup("设置管理器")]
        internal DebugOption debugOption;
#if UNITY_EDITOR
        [HideInInspector] public bool isRemote;

        [Button("关闭远端加载"), ShowIf("isRemote"), GUIColor(1, 0, 0), FoldoutGroup("设置管理器")]
        public void LocalButton() => AssetEditor.AddSceneToBuildSettings(isRemote = !isRemote);

        [Button("开启远端加载"), HideIf("isRemote"), GUIColor(0, 1, 0), FoldoutGroup("设置管理器")]
        public void RemoteButton() => AssetEditor.AddSceneToBuildSettings(isRemote = !isRemote);

        [ShowInInspector, LabelText("游戏对象管理"), FoldoutGroup("对象管理器")]
        private Dictionary<string, IPool> pools => PoolManager.pools;

        [ShowInInspector, LabelText("游戏组件管理"), FoldoutGroup("对象管理器")]
        private Dictionary<ICharacter, Dictionary<Type, ScriptableObject>> characters => CtrlManager.characters;

        [ShowInInspector, LabelText("资源管理数据"), FoldoutGroup("常用管理器")]
        private Dictionary<string, AssetBundle> depends => AssetManager.depends;

        [ShowInInspector, LabelText("资源管理数据"), FoldoutGroup("常用管理器")]
        private Dictionary<string, (string, string)> assets => AssetManager.assets;

        [ShowInInspector, LabelText("事件管理数据"), FoldoutGroup("常用管理器")]
        private Dictionary<Type, HashSet<IEvent>> observers => EventManager.observers;

        [ShowInInspector, LabelText("完成计时队列"), FoldoutGroup("计时器管理器")]
        private HashSet<Timer> timerFinish => TimerManager.finishList;

        [ShowInInspector, LabelText("正在计时队列"), FoldoutGroup("计时器管理器")]
        private LinkedList<Timer> timerList => TimerManager.timerList;

        [ShowInInspector, LabelText("整数数据管理"), FoldoutGroup("数据管理器")]
        private Dictionary<Type, Dictionary<int, IData>> intDataDict => DataManager<int>.dataDict;

        [ShowInInspector, LabelText("字符数据管理"), FoldoutGroup("数据管理器")]
        private Dictionary<Type, Dictionary<string, IData>> strDataDict => DataManager<string>.dataDict;

        [ShowInInspector, LabelText("枚举数据管理"), FoldoutGroup("数据管理器")]
        private Dictionary<Type, Dictionary<Enum, IData>> enmDataDict => DataManager<Enum>.dataDict;

        [ShowInInspector, LabelText("面板数据管理"), FoldoutGroup("面板管理器")]
        private Dictionary<Type, IPanel> panels => UIManager.panels;

        [ShowInInspector, LabelText("面板层级配置"), FoldoutGroup("面板管理器")]
        private Dictionary<Type, Transform> layers => UIManager.layers;

        [ShowInInspector, LabelText("背景音乐大小"), FoldoutGroup("音效管理器")]
        private float soundVolume => AudioManager.soundVolume;

        [ShowInInspector, LabelText("游戏音乐大小"), FoldoutGroup("音效管理器")]
        private float audioVolume => AudioManager.audioVolume;

        [ShowInInspector, LabelText("完成音效队列"), FoldoutGroup("音效管理器")]
        private HashSet<AudioSource> audioFinish => AudioManager.finishList;

        [ShowInInspector, LabelText("播放音效列表"), FoldoutGroup("音效管理器")]
        private HashSet<AudioSource> audioList => AudioManager.audioList;
#endif
    }

    internal static class Log
    {
        private static readonly Dictionary<DebugOption, string> options = new Dictionary<DebugOption, string>()
        {
            { DebugOption.Json, "JsonManager " },
            { DebugOption.Pool, "PoolManager " },
            { DebugOption.Data, "DataManager " },
            { DebugOption.Scene, "SceneManager " },
            { DebugOption.Asset, "AssetManager " },
            { DebugOption.Audio, "AudioManager " },
            { DebugOption.Timer, "TimerManager " },
            { DebugOption.Event, "EventManager " },
        };


        public static void Info(DebugOption option, string message)
        {
            if (!GlobalManager.Runtime) return;
            if ((GlobalManager.Instance.debugOption & option) == DebugOption.None) return;
            Debug.Log(options.TryGetValue(option, out var value) ? value.Sky() + message : message);
        }
    }

    [Flags]
    internal enum DebugOption
    {
        None = 0,
        Json = 1,
        Pool = 2,
        Data = 4,
        Scene = 8,
        Asset = 16,
        Audio = 32,
        Timer = 64,
        Event = 128,
    }
}