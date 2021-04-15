﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniModules.UniGame.Core.Runtime.Common;
using UniModules.UniGame.RemoteData.MutableObject;

namespace UniModules.UniGame.RemoteData.RemoteData
{
    public interface IRemoteObjectHandler<T> : IDisposableLifetimeContext
    {
        object DeleteValueObject { get; }
        
        T Object { get; }
        
        UniTask<RemoteObjectHandler<T>> LoadData(Func<T> initialDataProvider = null);
        
        RemoteDataChange CreateChange(string fieldName, object fieldValue);
        
        UniTask ApplyChange(RemoteDataChange change);
        
        UniTask ApplyChangesBatched(List<RemoteDataChange> changes);
        
        UniTask ClearData();
        
        void ApplyChangeLocal(RemoteDataChange change);
        string GetDataId();
        string GetFullPath();
    }
}