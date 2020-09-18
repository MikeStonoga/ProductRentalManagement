using PRM.Domain.Rents;

namespace PRM.UseCases.Renters.GetLastRenterRents
{
    public class GetLastRenterRentResult
    {
        public Rent LastRenterRent { get; set; }

        public GetLastRenterRentResult()
        {
            
        }
        public GetLastRenterRentResult(Rent lastRenterRent)
        {
            LastRenterRent = lastRenterRent;
        }

    }
}