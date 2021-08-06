using UniModules.UniGame.UniGame;

namespace UniModules.UniGame.RemoteData
{
    using System;
    using Firebase.Firestore;
    using UniModules.UniCore.Runtime.ReflectionUtils;
    using UniModules.UniGame.Core.Runtime.DataStructure;
    using UniModules.UniGame.Core.Runtime.SerializableType;
    using RemoteData;
    using UnityEngine;
    
    [Serializable]
    public class DefaultRemoteObjectFactory : ReactiveItemFactory, IVerifiable
    {
        public RemoteObjectMap remoteObjectMap = new RemoteObjectMap();

        public sealed override bool Validate(Type data)
        {
            return remoteObjectMap.ContainsKey(data);
        }

        public sealed override IReactiveRemoteObject<TData> CreateObject<TData>()
        {
            if (!remoteObjectMap.TryGetValue(typeof(TData), out var remoteType))
                return null;

            var remoteObject = Activator.CreateInstance(remoteType);
            return remoteObject as IReactiveRemoteObject<TData>;
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Verify()
        {
            remoteObjectMap.Clear();
            var fireData = this.GetAllWithAttributes<FirestoreDataAttribute>();
            foreach (var type in fireData)
            {
                var genericRemoteData = ReactiveRemoteDataTool.GetFirstReactiveModelType(type);
                if (genericRemoteData == null)
                    continue;

                remoteObjectMap[type] = genericRemoteData;
            
                Debug.Log($"REACTIVE MODEL FOR {type.GetFormattedName()} : {genericRemoteData.GetFormattedName()}");
            }
        }
    }


    [Serializable]
    public class RemoteObjectMap : SerializableDictionary<SType, SType>
    {
    
    }

    [Serializable]
    public class RemoteObjectReference
    {
        public SType remoteObject;

        public SType dataType;
    }
}