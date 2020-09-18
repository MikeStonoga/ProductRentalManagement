using PRM.UseCases.Rents.FinishRents;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos.FinishRents
{
    public class FinishRentOutput : FinishRentResult
    {
        public FinishRentOutput()
        {
        }

        public FinishRentOutput(FinishRentResult finishRentResult) : base(finishRentResult.ValueToPay)
        {
        }
    }
}