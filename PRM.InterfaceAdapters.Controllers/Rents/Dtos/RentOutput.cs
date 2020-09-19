using System;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Rents;
using PRM.Domain.Rents.Enums;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos
{
    public class RentOutput
    {
        Guid Id { get; }
        string Name { get; }
        string Code { get; }
        public Guid CreatorId { get; private set; }
        public DateTime CreationTime { get; private set; }
        public Guid? LastModifierId { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid RenterId { get; set; }
        public DateRange RentPeriod { get; set; }
        private RentStatus Status { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal DailyLateFee { get; set; }
        public bool WasProductDamaged { get; set; }
        public decimal DamageFee { get; set; }
        public decimal Discount { get; set; }
        public int RentedProductsCount { get; set; }
        public int LateDays { get; }
        public decimal AverageTicket { get; }

        public decimal AverageTicketWithDiscount { get; }

        public decimal AverageTicketWithoutFees { get; }

        public decimal AverageTicketWithoutFeesWithDiscount { get; }
        
        private RentOutput() {}
        public RentOutput(Rent rent)
        {
            Id = rent.Id;
            Name = rent.Name;
            Code = rent.Code;
            CreatorId = rent.CreatorId;
            CreationTime = rent.CreationTime;
            LastModifierId = rent.LastModifierId;
            LastModificationTime = rent.LastModificationTime;
            RenterId = rent.RenterId;
            RentPeriod = rent.RentPeriod;
            DailyPrice = rent.DailyPrice;
            DailyLateFee = rent.DailyLateFee;
            WasProductDamaged = rent.WasProductDamaged;
            DamageFee = rent.DamageFee;
            Discount = rent.Discount;
            RentedProductsCount = rent.RentedProductsCount;
            LateDays = rent.LateDays;
            AverageTicket = rent.AverageTicket;
            AverageTicketWithDiscount = rent.AverageTicketWithDiscount;
            AverageTicketWithoutFees = rent.AverageTicketWithoutFees;
            AverageTicketWithoutFeesWithDiscount = rent.AverageTicketWithoutFeesWithDiscount;
        }
    }
}