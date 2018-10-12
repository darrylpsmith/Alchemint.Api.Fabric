using System;

namespace Alchemint.Bar
{
    public interface IBarToken
    {
        string CurrentWallet { get; set; }
        string Id { get; set; }
        DateTime IssueTime { get; set; }
        string OriginatorWalletAddress { get; set; }
        Int64 TokenType { get; set; }
    }
}