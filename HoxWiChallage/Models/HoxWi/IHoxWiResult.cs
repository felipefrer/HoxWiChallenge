using System;

namespace HoxWiChallage.Models.HoxWi
{
    public interface IHoxWiResult
    {
        string Header { get; set; }
        bool Success { get; set; }
        string Error { get; set; }
        object[] Results { get; set; }
    }
}
