using System.Diagnostics;

namespace HeperFiberli.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public static ErrorViewModel FromActivity() => new() { RequestId = Activity.Current?.Id ?? Guid.NewGuid().ToString() };
}
