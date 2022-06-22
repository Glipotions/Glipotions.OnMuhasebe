using System;
using System.Threading.Tasks;

namespace Glipotions.Blazor.Core.Services;

public interface ICoreMessageService
{
    Task ConfirmMessage(string message, Action action, string title = null);
}