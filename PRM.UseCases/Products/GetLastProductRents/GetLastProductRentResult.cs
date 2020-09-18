using PRM.Domain.Rents;

namespace PRM.UseCases.Products.GetLastProductRents
{
    public class GetLastProductRentResult
    {
        public Rent LastProductRent { get; set; }

        public GetLastProductRentResult()
        {
            
        }
        public GetLastProductRentResult(Rent lastProductRent)
        {
            LastProductRent = lastProductRent;
        }

    }
}