﻿using SharedModule.UIModule.Signals;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.MVP.Installers
{
    public class test : MonoBehaviour, IInitializable
    {
        public SignalBus _SignalBus;
        
        [Inject]
        public void Construct(SignalBus __signalBus)
        {
            _SignalBus = __signalBus;
            
            //_SignalBus.TryFire(new NextWriteTextSignal());
        }
        
        public void Initialize()
        {
           _SignalBus.TryFire(new NextWriteTextSignal());
           _SignalBus.TryFire(new NextStaticTextSignal());
        }
    }
}