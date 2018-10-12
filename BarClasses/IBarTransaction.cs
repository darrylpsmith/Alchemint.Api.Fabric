using System;

namespace Alchemint.Bar
{
    public interface IBarTransaction
    {
        string SourceWalletAddress { get; set; }
        string TargetWalletAddress { get; set; }
        float TokenAmount { get; set; }
        DateTime TxDate { get; set; }
    }
}