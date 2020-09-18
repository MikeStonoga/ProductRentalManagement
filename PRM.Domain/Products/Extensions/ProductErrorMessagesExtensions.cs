using System;
using System.Collections.Generic;
using System.Linq;

namespace PRM.Domain.Products.Extensions
{
    public static class ProductErrorMessagesExtensions
    {
        public static string GetProductsWithErrorMessage(this List<Product> productsToRent, string productErrorMessage, Func<Product, bool> errorCondition)
        {
            var productsWithError = productsToRent.Where(errorCondition).ToList();
            var exceptionMessage = productErrorMessage;
                
            foreach (var product in productsWithError)
            {
                exceptionMessage += $" \n {product.Code} - {product.Name} - {product.Description} - {product.IsAvailable.ToString()}";
            }

            return exceptionMessage;
        }
    }
}