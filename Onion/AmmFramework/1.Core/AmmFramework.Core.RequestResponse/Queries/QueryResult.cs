﻿using AmmFramework.Core.RequestResponse.Common;

namespace AmmFramework.Core.RequestResponse.Queries;

/// <summary>
/// نتیجه یک کوئری را بازگشت می‌دهد
/// </summary>
/// <typeparam name="TData"></typeparam>
public sealed class QueryResult<TData> : ApplicationServiceResult
{
    public TData? _data;
    public TData? Data
    {
        get
        {
            return _data;
        }
    }
}

