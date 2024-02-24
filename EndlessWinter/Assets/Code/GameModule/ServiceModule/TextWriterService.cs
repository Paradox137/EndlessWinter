using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameModule.SettingsModule;
using TMPro;
using UnityEngine;

namespace GameModule.ServiceModule
{
    public enum WriteMode
    {
        NormalMode,
        SpeedMode,
    }
    
    public class TextWriterService
    {
        private WriteMode _writeMode;
        private readonly CharsTimeDelaySettings _charsTimeDelaySettings;

        public TextWriterService(CharsTimeDelaySettings __charsTimeDelaySettings)
        {
            _charsTimeDelaySettings = __charsTimeDelaySettings;
            _writeMode = WriteMode.NormalMode;
        }

        public void OutputText(TextMeshProUGUI __screenText, string __text)
        {
            __screenText.text = __text;
        }
        
        public async void WriteTextLetterByLetter(TextMeshProUGUI __screenText, string __text, CancellationToken __token)
        {
            __screenText.text = String.Empty;
            
            Debug.Log(__text);
            
            foreach (char letter in __text)
            {
                __screenText.text += letter;

                await PauseBetweenChars(letter, __token);
            }
        }

        private async UniTask PauseBetweenChars(char __letter, CancellationToken __token)
        {
            switch (__letter)
            {
                case '.' or '!' or '?':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetDefaultDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                case ':' or ',' or ';' or '"' or '~' or '`':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetDefaultDelay(_writeMode)),
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                case ' ':
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetDefaultDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
                default:
                    await UniTask.Delay(TimeSpan.FromMilliseconds(_charsTimeDelaySettings.GetDefaultDelay(_writeMode)), 
                        DelayType.DeltaTime, cancellationToken: __token);
                    break;
            }
        }

        public void ChangeMode(WriteMode __mode) => _writeMode = __mode;
    }
}