using System;

namespace Alchemint.Bar
{
    public interface IBarWallet
    {
        DateTime CreationTime { get; set; }
        string OwnerId { get; set; }
        string PrivateKey { get; set; }
        string PublicKey { get; set; }
        string ReceiveAddress { get; set; }
    }
}