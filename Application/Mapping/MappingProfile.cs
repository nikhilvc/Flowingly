using AutoMapper;
using Flowingly.Domain.DTOs;
using Flowingly.Domain.Entities;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Flowingly.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<XmlDocument, Expense>()
               .ForMember(dest => dest.CostCentre, opt => opt.MapFrom(src => GetNodeValue(src, "/Reservation/expense/cost_centre") ?? "UNKNOWN"))
               .ForMember(dest => dest.Total, opt => opt.MapFrom(src => decimal.Parse(GetNodeValue(src, "/Reservation/expense/total") ?? "0")))
               .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => GetNodeValue(src, "/Reservation/expense/payment_method")))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => GetNodeValue(src, "/Reservation/description")))
               .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => GetNodeValue(src, "/Reservation/vendor")))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => GetDateTimeNodeValue(src, "/Reservation/date")));

            CreateMap<Expense, ExpenseDto>();
        }

        private static string? GetNodeValue(XmlDocument doc, string xpath)
        {
            var node = doc.SelectSingleNode(xpath);
            return node?.InnerText;
        }

        private static DateTime GetDateTimeNodeValue(XmlDocument doc, string xpath)
        {
            var node = doc.SelectSingleNode(xpath);
            if (node != null && DateTime.TryParse(node.InnerText, out DateTime result))
            {
                return result;
            }

            return DateTime.MinValue;
        }
    }
}
