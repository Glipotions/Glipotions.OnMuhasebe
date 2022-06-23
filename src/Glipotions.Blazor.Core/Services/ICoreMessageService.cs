using System;
using System.Threading.Tasks;

namespace Glipotions.Blazor.Core.Services;
/// <ÖZET>
/// Mesajların Servisidir.
public interface ICoreMessageService
{
    /// <ÖZET>
    /// Onay mesajı ve onay olumlu ise action çalıştırılır
    Task ConfirmMessage(string message, Action action, string title = null);
}