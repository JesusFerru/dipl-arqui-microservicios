using Finantech.Solutions.Core.Models.Enums;

namespace FinanTech.Solutions.App.Handler;

public class ReportMenuConfig
{
    public string RawDataInput { get; set; } = string.Empty;
    public UserType SelectedUserType { get; set; }
    public string SelectedFormat { get; set; } = string.Empty;

    // Optional decorators
    public bool ApplyHeader { get; set; }
    public bool ApplyWatermark { get; set; }
    public bool ApplyEncryption { get; set; }
    public bool ApplyCompression { get; set; }
}
