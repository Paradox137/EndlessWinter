using UnityEngine;
using Zenject;

namespace GameModule.UIModule.Undefined
{
    public class testInstaller : MonoInstaller
    {
        [SerializeField] private test _test;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<test>().FromInstance(_test).AsSingle();
        }
    }
}