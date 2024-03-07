using System.Collections.Generic;
using System.Linq;
using GameModule.DataModule;
using GameModule.DataModule.Novel;
using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using UnityEngine;

namespace GameModule.StorageModule
{
    public class NovelStorage
    {
        private ChapterFlow _chapter;
        
        private ushort _dialogueSavedPart;
        private ushort _dialogueSavedFlow;
        private ushort _chapterSavedPart;
        public ChapterFlow Flow => _chapter;
        public ushort DialogueSavedFlow
        {
            get => _dialogueSavedFlow;
            set => _dialogueSavedFlow = value;
        }

        public NovelStorage()
        {
            
        }
        
        public void SetData(DialogueFlow[] __dialogueFlows, (ushort, ushort, ushort) __savePlace)
        {
            _chapter = new ChapterFlow(__dialogueFlows.ToList(), __savePlace.Item2, __savePlace.Item1);
            
            _chapterSavedPart = __savePlace.Item1;
            _dialogueSavedPart = __savePlace.Item2;
            _dialogueSavedFlow = __savePlace.Item3;
        }
        
        public ControlMode SkipToSaveData()
        {
            if (_dialogueSavedPart > 0)
            {
                for (int i = 0; i < _chapter.Dialogues.Count; i++)
                    _chapter.Dialogues.RemoveAt(i);
            }
            
                
            ushort coupledFlow = _dialogueSavedFlow;

            if (_dialogueSavedFlow >= _chapter.Dialogues[_dialogueSavedPart].StartFlow.Count)
            {
                coupledFlow -= (ushort) _chapter.Dialogues[_dialogueSavedPart].StartFlow.Count;
                
                RemoveItemsActorsAndFlowsEnd(_chapter.Dialogues[_dialogueSavedPart].EndFlow, 
                    _chapter.Dialogues[_dialogueSavedPart].Actors, coupledFlow);

                _chapter.Dialogues[_dialogueSavedPart].StartFlow.Clear();
                _chapter.Dialogues[_dialogueSavedPart].PositiveFlow.Clear();
                _chapter.Dialogues[_dialogueSavedPart].NegativeFlow.Clear();
                _chapter.Dialogues[_dialogueSavedPart].EndFlow.DequeueMulty(coupledFlow);
                
                
                return ControlMode.End;
            }
            
            RemoveItemsActorsAndFlowsStart(_chapter.Dialogues[_dialogueSavedPart].StartFlow,
                _chapter.Dialogues[_dialogueSavedPart].Actors, _dialogueSavedFlow);

            return ControlMode.Start;
        }
        public bool TryIncreaseDialoguePart()
        {
            if (Flow.Dialogues.Count <= _dialogueSavedPart + 1)
                return false;
            
            _dialogueSavedPart++;
            return true;
        }
        private void RemoveItemsActorsAndFlowsStart(Queue<KeyValuePair<ActorType, Sprite>> __flow, List<Actor> __actors, int __count)
        {
            for (int i = 0; i < __count; i++)
            {
                KeyValuePair<ActorType, Sprite> pair = __flow.Dequeue();
                
                __actors.Find(_ => _.ActorName == pair.Key).StartReplicas.Dequeue();
            }
        }
        private void RemoveItemsActorsAndFlowsEnd(Queue<KeyValuePair<ActorType, Sprite>> __flow, List<Actor> __actors, int __count)
        {
            for (int i = 0; i < __count; i++)
            {
                KeyValuePair<ActorType, Sprite> pair = __flow.Dequeue();
                
                __actors.Find(_ => _.ActorName == pair.Key).EndReplicas.Dequeue();
            }
        }

    }
}