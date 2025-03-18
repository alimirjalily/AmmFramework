using AmmFramework.Core.Contracts.ApplicationServices.Commands;
using AmmFramework.Core.RequestResponse.Commands;
using AmmFramework.Core.RequestResponse.Common;
using AmmFramework.Utilities;

namespace AmmFramework.Core.ApplicationService.Commands;

public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
    where TCommand : ICommand<TData>
{

    protected readonly FrameworkServices _frameworkServices;
    protected readonly CommandResult<TData> result = new();
    public CommandHandler(FrameworkServices frameworkServices)
    {
        _frameworkServices = frameworkServices;
    }

    public abstract Task<CommandResult<TData>> Handle(TCommand command);
    protected virtual Task<CommandResult<TData>> OkAsync(TData data)
    {
        result._data = data;
        result.Status = ApplicationServiceStatus.Ok;
        return Task.FromResult(result);
    }
    protected virtual CommandResult<TData> Ok(TData data)
    {
        result._data = data;
        result.Status = ApplicationServiceStatus.Ok;
        return result;
    }
    protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return Task.FromResult(result);
    }

    protected virtual CommandResult<TData> Result(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return result;
    }

    protected void AddMessage(string message)
    {
        result.AddMessage(_frameworkServices.Translator[message]);
    }
    protected void AddMessage(string message, params string[] arguments)
    {
        result.AddMessage(_frameworkServices.Translator[message, arguments]);
    }
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{
    protected readonly FrameworkServices _frameworkServices;
    protected readonly CommandResult result = new();
    public CommandHandler(FrameworkServices frameworkServices)
    {
        _frameworkServices = frameworkServices;
    }
    public abstract Task<CommandResult> Handle(TCommand command);

    protected virtual Task<CommandResult> OkAsync()
    {
        result.Status = ApplicationServiceStatus.Ok;
        return Task.FromResult(result);
    }

    protected virtual CommandResult Ok()
    {
        result.Status = ApplicationServiceStatus.Ok;
        return result;
    }

    protected virtual Task<CommandResult> ResultAsync(ApplicationServiceStatus status)
    {
        result.Status = status;
        return Task.FromResult(result);
    }
    protected virtual CommandResult Result(ApplicationServiceStatus status)
    {
        result.Status = status;
        return result;
    }
    protected void AddMessage(string message)
    {
        result.AddMessage(_frameworkServices.Translator[message]);
    }
    protected void AddMessage(string message, params string[] arguments)
    {
        result.AddMessage(_frameworkServices.Translator[message, arguments]);
    }
}

