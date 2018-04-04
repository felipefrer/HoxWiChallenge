using System;

namespace HoxWiChallage.Models.HoxWi
{
    public class HoxWiResult : IHoxWiResult
    {
        public string Header { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public object[] Results { get; set; }
    }
}