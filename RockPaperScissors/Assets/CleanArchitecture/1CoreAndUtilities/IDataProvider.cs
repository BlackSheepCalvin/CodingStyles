using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataProvider
{
    public void RequestData<T>(string dataId, Action<T> callback);
}   
