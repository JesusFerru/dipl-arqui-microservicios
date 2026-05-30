using Finantech.Solutions.Core.Models.Enums;

namespace FinanTech.Solutions.App.Handler;

public class ReportMenuConfig
{
    public string RawDataInput { get; set; } = string.Empty;
    public UserType SelectedUserType { get; set; }
    public string SelectedFormat { get; set; } = string.Empty;
}
