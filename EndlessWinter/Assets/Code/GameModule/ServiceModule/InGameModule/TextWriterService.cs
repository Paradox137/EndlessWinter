using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameModule.ConfigsModule;
using GameModule.DataModule;
using TMPro;
using UnityEngine;

namespace GameModule.ServiceModule.InGameModule
{
    public enum WriteMode
    {
        NormalMode,
        SpeedMode,
    }
    
    public class TextWriterService
    {
        private WriteMode _writeMode;
        private readonly CharsTimeDelayConfig _charsTimeDelaySettings;
        private readonly TextViewConfig _viewConfig;

        private bool _isBusy = false;
        public bool IsBusy => _isBusy;
        public WriteMode Mode => _writeMode;
        public TextWriterService(CharsTimeDelayConfig __charsTimeDelaySettings, TextViewConfig __viewConfig)
        {
            _charsTimeDelaySettings = __charsTimeDelaySettings;
            _viewConfig = __viewConfig;
            
            _writeMode = WriteMode.NormalMode;
            _isBusy = false;
        }
        
        public void ChangeMode(WriteMode __mode) => _writeMode = __mode;

        public void OutputActorText(TextMeshProUGUI __screenText, string __text)
        {
            __screenText.color = _viewConfig.ActorsTexts.MainTextColor;
            __screenText.text = __text;
        }
        public void OutputActorNameText(TextMeshProUGUI __screenText, string __text)
        {
            __screenText.color = _viewConfig.ActorsTexts.NamesItems.First(_ => _.ActorType == __text.GetActorType()).NameColor;
            __screenText.text = __text;
        }
        public async void WriteTextLetterByLetter(TextMeshProUGUI __screenText, string __text, CancellationToken __token)
        {
            __token.Register(() =>
            {
                _isBusy = false;
                _writeMode = WriteMode.NormalMode;
            });
            
            _isBusy = true;
            
            __screenText.text = String.Empty;
            
            Debug.Log(__text);
            
            foreach (char letter in __text)
            {
                __screenText.text += letter;

                await PauseBetweenChars(letter, __token);
            }
            
            _isBusy = false;
        }

        private async UniTask PauseBetweenChars(char __letter, CancellationToken __token)
        {
            switch (__letter)
            {
                case '.' or '!' or '?':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetEndDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                case ':' or ',' or ';' or '"' or '~' or '`':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetSpecialDelay(_writeMode)),
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                case ' ':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetSpaceDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                default:
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetDefaultDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
            }
        }
        
    }
}