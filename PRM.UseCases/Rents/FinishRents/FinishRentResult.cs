namespace PRM.UseCases.Rents.FinishRents
{
    public class FinishRentResult
    {
        public decimal ValueToPay { get; set; }
        
        public FinishRentResult()
        {
        }
        
        public FinishRentResult(decimal valueToPay)
        {
            ValueToPay = valueToPay;
        }
    }
}