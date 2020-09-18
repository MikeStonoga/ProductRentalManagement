using PRM.UseCases.Renters.GetLastRenterRents;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos.GetLastRenterRents
{
    public class GetLastRenterRentOutput : GetLastRenterRentResult
    {

        public GetLastRenterRentOutput()
        {
            
        }

        public GetLastRenterRentOutput(GetLastRenterRentResult result)
        {
            LastRenterRent = result.LastRenterRent;
        }
    }
}