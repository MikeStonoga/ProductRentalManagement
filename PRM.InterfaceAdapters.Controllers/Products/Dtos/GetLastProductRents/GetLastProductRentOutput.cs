using PRM.UseCases.Products.GetLastProductRents;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos.GetLastProductRents
{
    public class GetLastProductRentOutput : GetLastProductRentResult
    {

        public GetLastProductRentOutput()
        {
            
        }

        public GetLastProductRentOutput(GetLastProductRentResult result)
        {
            LastProductRent = result.LastProductRent;
        }
    }
}