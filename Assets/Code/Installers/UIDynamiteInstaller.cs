﻿using UnityEngine;
using Zenject;

namespace Code
{
    public class UIDynamiteInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject UIDynamitePrefab;

        [Inject] private DynamiteCounter.Settings _dynamiteCounterSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIDynamiteManager>().AsSingle();

            Container.BindMemoryPool<UIDynamite, UIDynamite.Pool>()
                .WithInitialSize(_dynamiteCounterSettings.TotalNumDynamites)
                .FromComponentInNewPrefab(UIDynamitePrefab)
                .UnderTransform(transform);
        }
    }
}