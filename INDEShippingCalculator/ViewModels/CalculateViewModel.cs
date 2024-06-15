using System.Collections.Generic;

namespace INDEShipping.ViewModels
{
    public class CalculateViewModel
    {
        public decimal TotalWeight { get; set; }
        public bool IsDifficultAccess { get; set; }
        public List<OfferResultViewModel>? Results { get; set; }

        public CalculateViewModel()
        {
            Results = new List<OfferResultViewModel>();
        }
    }
}
