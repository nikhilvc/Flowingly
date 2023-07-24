using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flowingly.Domain.Entities
{
    public class ExpenseDetails
    {
        [JsonPropertyName("cost_centre")]
        public string CostCentre { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }
    }
}
