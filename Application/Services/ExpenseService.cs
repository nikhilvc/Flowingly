using AutoMapper;
using Flowingly.Application.Exceptions;
using Flowingly.Application.Interfaces;
using Flowingly.Domain.DTOs;
using Flowingly.Domain.Entities;


namespace Flowingly.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IExtractXmlService _extractXmlService;

        public ExpenseService(IMapper mapper, IExtractXmlService extractXmlService)
        {
            _mapper = mapper;
            _extractXmlService = extractXmlService;
        }

        public ValidationResult ExtractExpense(string text)
        {
            try
            {
                var xmlDocument = _extractXmlService.GetXmlFromText(text);

                var expense = _mapper.Map<Expense>(xmlDocument);

                if (expense == null) { return ValidationResult.CreateError("Invalid data."); }

                // Calculate sales tax and total excluding tax
                var total = expense.Total;
                decimal salesTaxRate = 0.1m; // Assuming a 10% sales tax rate
                var salesTax = total * salesTaxRate;
                var totalExcludingTax = total - salesTax;

                var expenseDto = _mapper.Map<ExpenseDto>(expense);
                expenseDto.SalesTax = salesTax;
                expenseDto.TotalExcludingTax = totalExcludingTax;

                return ValidationResult.CreateSuccess(expenseDto);
            }
            catch (Exception ex)
            {
                return ValidationResult.CreateError(ex.Message);
            }
        }
    }

}
