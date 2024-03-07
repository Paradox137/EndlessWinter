using System;
using System.Collections.Generic;
using System.Linq;
using GameModule.DataModule;
using GameModule.DataModule.Novel;
using GameModule.EntityModule;
using GameModule.ProviderModule;
using GameModule.ProviderModule.Sprite;
using GameModule.ProviderModule.Text;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.StorageModule;
using GameModule.UIModule.Signals;
using GameModule.UIModule.Window;
using SharedModule.CollectionModule;
using SharedModule.CustomizeModule;
using UnityEngine;
using Zenject;

namespace GameModule.ServiceModule.InGameModule
{
    public enum ControlMode
    {
        Positive,
        Negative,
        Start,
        End,
    }
    // todo: * по маленьким классам изображения текст 2 метода инит и некст и интерфейты сделать к ним и тут от интерфейса
    public class NovelFlowController : IDisposable
    {
        private readonly NovelStorage _storage;
        private readonly SignalBus _signalBus;
        private readonly PlayerSaveLoadSystem _saveLoadSystem;
        
        [Inject(Id = ProviderID.ActorNovelText)]
        private readonly TextProvider _actorTextProvider;
        [Inject(Id = ProviderID.ActorNameText)]
        private readonly TextProvider _actorNameTextProvider;
        [Inject(Id = ProviderID.ActorSprite)]
        private readonly SpriteProvider _actorSpriteProvider;
        [Inject(Id = ProviderID.MainSprite)]
        private readonly SpriteProvider _mainSpriteProvider;
        
        private ControlMode _mode;
        private bool _questChoiceFlag;
        public event Action onNextChapter;
        public event Action onEndGame;
        public event Action<List<(PerkType, int)>> onPlayerChoiced;
        
        [Inject]
        public NovelFlowController(NovelStorage __novelStorage, SignalBus __signalBus, PlayerSaveLoadSystem __saveLoadSystem)
        {
            _storage = __novelStorage;
            _signalBus = __signalBus;
            _saveLoadSystem = __saveLoadSystem;

            _questChoiceFlag = false;
            _mode = ControlMode.Start;
            SubscribeSignals();
        }
        private void SubscribeSignals()
        {
            _signalBus.Subscribe<NextNovelData>(_ => ValidateNovelData());
        }
        
        public void SkipFlowUntilSavePlace()
        {
            _mode = _storage.SkipToSaveData();
            Debug.Log(_mode);
        }

        private void OnPlayerChoice(ControlMode __controlMode)
        {
            _mode = __controlMode;

            if (_mode == ControlMode.Negative)
            {
                UpgradePerksWithSave(_storage.Flow.Dialogues[_storage.Flow.DialoguePart].GamerChoice.NegativeInfluence);
                onPlayerChoiced?.Invoke(_storage.Flow.Dialogues[_storage.Flow.DialoguePart].GamerChoice.NegativeInfluence);
            }
            else
            {
                Debug.Log("YYYYYYYYYYYY");
                UpgradePerksWithSave(_storage.Flow.Dialogues[_storage.Flow.DialoguePart].GamerChoice.PositiveInfluence);
                onPlayerChoiced?.Invoke(_storage.Flow.Dialogues[_storage.Flow.DialoguePart].GamerChoice.PositiveInfluence);
            }
            
            ValidateNovelData();
        }
        
        private void UpgradePerksWithSave(List<(PerkType, int)> __influenece)
        {
            for (int i = 0; i < _saveLoadSystem.GetPlayerData().PerkEntities.Count; i++)
            {
                for (int j = 0; j < __influenece.Count; j++)
                {
                    PerkEntity perkEntity = _saveLoadSystem.GetPlayerData().PerkEntities[i];

                    if (perkEntity.Type == __influenece[j].Item1)
                        perkEntity.Value += __influenece[j].Item2;

                    _saveLoadSystem.GetPlayerData().PerkEntities[i] = perkEntity;
                }
            }

            _saveLoadSystem.Save();
        }
        public void ValidateNovelData()
        {
            if (_storage.DialogueSavedFlow == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].ActorQuestNumber && _questChoiceFlag == false 
                && (_mode != ControlMode.Positive && _mode != ControlMode.Negative))
            {
                _questChoiceFlag = true;
            }
            
            if (_storage.DialogueSavedFlow == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].ActorQuestNumber && _questChoiceFlag)
            {
                WindowsCollection.Get<QuestChoicePopupWindow>().Show(_storage.Flow.Dialogues[_storage.Flow.DialoguePart].GamerChoice, 
                    new Action<ControlMode>(OnPlayerChoice));
                
                _questChoiceFlag = false;
                
                return;
            }
            
            if (_mode == ControlMode.Positive)
            {
                Queue<KeyValuePair<ActorType, Sprite>> positiveFlow = _storage.Flow.Dialogues[_storage.Flow.DialoguePart].PositiveFlow;

                bool exists = positiveFlow.TryPeek(out KeyValuePair<ActorType, Sprite> _);

                if (exists)
                {
                    GetNextNovelData();
                    return;
                }
                else
                    _mode = ControlMode.End;
            }

            else if (_mode == ControlMode.Negative)
            {
                Queue<KeyValuePair<ActorType, Sprite>> negativeFlow = _storage.Flow.Dialogues[_storage.Flow.DialoguePart].NegativeFlow;

                bool exists = negativeFlow.TryPeek(out KeyValuePair<ActorType, Sprite> _);

                if (exists)
                {
                    GetNextNovelData();
                    return;
                }
                else
                    _mode = ControlMode.End;
            }

            if (_storage.DialogueSavedFlow + 1 >= _storage.Flow.Dialogues[_storage.Flow.DialoguePart].MaxFlow)
            {
                bool canGetData = true;
                    
                canGetData = _storage.TryIncreaseDialoguePart();

                if (canGetData == false)
                {
                    if (_storage.Flow.ChapterNumber + 1 > 1)
                    {
                        onEndGame?.Invoke();
                        return;
                    }
                    
                    onNextChapter?.Invoke();
                    return;
                }
                
                else
                {
                    _mode = ControlMode.Start;
                    _storage.DialogueSavedFlow = 0;
                    _storage.Flow.DialoguePart++;
                    InitNovelData();
                    return;
                }
            }
                
            else
            {
                _mode = ControlMode.Start;
            }
            

            if (_mode == ControlMode.Start)
            {
                bool exists = _storage.Flow.Dialogues[_storage.Flow.DialoguePart].StartFlow.TryPeek(out KeyValuePair<ActorType, Sprite> _);

                if (exists == false)
                {
                    _mode = ControlMode.End;
                }
            }
            
            if (_mode == ControlMode.End)
            {
                bool exists = _storage.Flow.Dialogues[_storage.Flow.DialoguePart].EndFlow.TryPeek(out KeyValuePair<ActorType, Sprite> _);
                
                if (exists == false)
                    _mode = ControlMode.Start;
            }

            if (_questChoiceFlag != true && _mode!=ControlMode.Negative && _mode != ControlMode.Positive)
            {
                _storage.DialogueSavedFlow++;
                _storage.Flow.Dialogues[_storage.Flow.DialoguePart].CurrentFlow++;
                SavePlayerFlowData();
                GetNextNovelData();
            }
        }
        
        private void GetNextNovelData()
        {
            Sprite mainSprite = GetMainSprite();
            if (_mainSpriteProvider.Sprite != mainSprite)
            {
                _mainSpriteProvider.Sprite = mainSprite;
                _signalBus.Fire<NextMainImageSignal>();
            }
            else
                _mainSpriteProvider.Sprite = mainSprite;

            Sprite actorSprite = GetNextActorSprite();
            if (_actorSpriteProvider.Sprite != actorSprite)
            {
                _actorSpriteProvider.Sprite = actorSprite;
                _signalBus.Fire<NextActorImageSignal>();
            }
            else
                _actorSpriteProvider.Sprite = actorSprite;
            
            string actorNameText = GetNextActorNameText();
            if (_actorNameTextProvider.Text != actorNameText)
            {
                _actorNameTextProvider.Text = actorNameText;
                _signalBus.Fire<NextActorTextSignal>();
            }
            else
                _actorNameTextProvider.Text = actorNameText;

            string actorText = GetNextActorText();
            
            if (_actorTextProvider.Text != actorText)
            {
                _actorTextProvider.Text = actorText;
                _signalBus.Fire<NextWriteTextSignal>();
            }
            else
                _actorTextProvider.Text = actorText;
        }

        public void InitNovelData()
        {
            CustomDebug.WriteLine("Player Saved Place", $"Глава: {_storage.Flow.ChapterNumber}, " +
                $"Чаcть: {_storage.Flow.DialoguePart}, " +
                $"Указатель: {_storage.Flow.Dialogues[_storage.Flow.DialoguePart].CurrentFlow}", CustomDebugColors.Red);
            
            _mainSpriteProvider.Sprite = GetMainSprite();
            _actorSpriteProvider.Sprite = GetNextActorSprite();
            _actorNameTextProvider.Text = GetNextActorNameText();
            _actorTextProvider.Text = GetNextActorText();
            
            _signalBus.Fire<GameInitSignal>();
        }
        
        private Sprite GetNextActorSprite()
        {
            if (_mode == ControlMode.Start)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].StartFlow.Peek().Value;
            if (_mode == ControlMode.End)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].EndFlow.Peek().Value;
            if (_mode == ControlMode.Positive)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].PositiveFlow.Peek().Value;
            if (_mode == ControlMode.Negative)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].NegativeFlow.Peek().Value;
            
            throw new NotImplementedException("No Data");
        }


        public Sprite GetMainSprite()
        {
            return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].ImageFlow
                .Last(__i => __i.Key <= _storage.DialogueSavedFlow).Value;
        }
        
        public string GetNextActorNameText()
        {
            if (_mode == ControlMode.Start)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].StartFlow.Peek().Key.GetActorName();
            if (_mode == ControlMode.Positive)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].PositiveFlow.Peek().Key.GetActorName();
            if (_mode == ControlMode.Negative)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].NegativeFlow.Peek().Key.GetActorName();
            if (_mode == ControlMode.End)
                return _storage.Flow.Dialogues[_storage.Flow.DialoguePart].EndFlow.Peek().Key.GetActorName();
            
            throw new NotImplementedException("No Data");
        }
        
        public string GetNextActorText()
        {
            if (_mode == ControlMode.Start)
            {
                foreach (Actor a in _storage.Flow.Dialogues[_storage.Flow.DialoguePart].Actors)
                {
                    if (a.ActorName == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].StartFlow.Peek().Key)
                    {
                        _storage.Flow.Dialogues[_storage.Flow.DialoguePart].StartFlow.Dequeue();
                        return a.StartReplicas.Dequeue();
                    }
                }
            }
            if (_mode == ControlMode.Negative)
            {
                foreach (Actor a in _storage.Flow.Dialogues[_storage.Flow.DialoguePart].Actors)
                {
                    if (a.ActorName == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].NegativeFlow.Peek().Key)
                    {
                        _storage.Flow.Dialogues[_storage.Flow.DialoguePart].NegativeFlow.Dequeue();
                        return a.NegativeReplicas.Dequeue();
                    }
                }
            }
            if (_mode == ControlMode.Positive)
            {
                foreach (Actor a in _storage.Flow.Dialogues[_storage.Flow.DialoguePart].Actors)
                {
                    if (a.ActorName == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].PositiveFlow.Peek().Key)
                    {
                        _storage.Flow.Dialogues[_storage.Flow.DialoguePart].PositiveFlow.Dequeue();
                        return a.PositiveReplicas.Dequeue();
                    }
                }
            }
            if (_mode == ControlMode.End)
            {
                foreach (Actor a in _storage.Flow.Dialogues[_storage.Flow.DialoguePart].Actors)
                {
                    if (a.ActorName == _storage.Flow.Dialogues[_storage.Flow.DialoguePart].EndFlow.Peek().Key)
                    {
                        _storage.Flow.Dialogues[_storage.Flow.DialoguePart].EndFlow.Dequeue();
                        return a.EndReplicas.Dequeue();
                    }
                }
            }

            throw new NotImplementedException("No Data");
        }


        private void SavePlayerFlowData()
        {
            _saveLoadSystem.GetPlayerData().SavePlace = (_storage.Flow.ChapterNumber, _storage.Flow.DialoguePart,
                _storage.DialogueSavedFlow);
            _saveLoadSystem.Save();
            
            CustomDebug.WriteLine("Player Saved Place", $"Глава: {_storage.Flow.ChapterNumber}, " +
                $"Чаcть: {_storage.Flow.DialoguePart}, " +
                $"Указатель: {_storage.Flow.Dialogues[_storage.Flow.DialoguePart].CurrentFlow}", CustomDebugColors.Red);
        }
        public void Dispose()
        {
            Debug.Log("here");
            
            onNextChapter = null;
            onEndGame= null;
            onPlayerChoiced= null;
        }
    }
}