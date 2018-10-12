namespace Alchemint.Bar
{
    public interface IBarCreditCardDetails
    {
        string CardNumber { get; set; }
        string CvvNumber { get; set; }
        string ExpiryDate { get; set; }
        string NameOnCard { get; set; }
    }
}