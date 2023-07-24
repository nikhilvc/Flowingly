using Flowingly.Application.Exceptions;
using Flowingly.Domain.DTOs;

namespace Flowingly.Application.Interfaces
{
    public interface IExpenseService
    {
        ValidationResult ExtractExpense(string text);
    }
}
