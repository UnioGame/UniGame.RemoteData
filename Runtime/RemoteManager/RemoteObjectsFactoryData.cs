﻿using System;
using Cysharp.Threading.Tasks;
using UniModules.UniGame.Core.Runtime.ScriptableObjects;
using UniModules.UniGame.RemoteData.MutableObject;
using UniModules.UniGame.RemoteData.RemoteData;
using UniModules.UniGame.RemoteData.Runtime.RemoteManager.Abstract;
using UnityEngine;

namespace UniModules.UniGame.RemoteData.Runtime.RemoteManager
{
    [CreateAssetMenu(menuName = "UniGame/RemoteData/RemoteObjectsFactoryData", fileName = nameof(RemoteObjectsFactoryData))]
    public class RemoteObjectsFactoryData : ScriptableObject,IReactiveRemoteObjectFactory
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
#endif
        [SerializeReference]
        public IReactiveRemoteObjectFactory objectFactory = new ReactiveRemoteObjectFactory();

        public async UniTask<IReactiveRemoteObject<T>> Create<T>(IRemoteObjectHandler<T> dataHandler) where T : class
        {
            return await objectFactory.Create(dataHandler);
        }

        public async UniTask<IReactiveRemoteObject<T>> Create<T>(IRemoteObjectHandler<T> dataHandler, Func<T> defaultValue) where T : class
        {
            return await objectFactory.Create(dataHandler,defaultValue);
        }
    }
}