﻿using AmmFramework.Extensions.UsersManagement.Abstractions;
using AmmFramework.Infra.Data.Sql.Commands.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace AmmFramework.Infra.Data.Sql.Commands.Interceptors;

public class AddAuditDataInterceptor : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddShadowProperties(eventData);
        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddShadowProperties(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void AddShadowProperties(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        changeTracker.SetAuditableEntityPropertyValues(userInfoService);
    }
}
