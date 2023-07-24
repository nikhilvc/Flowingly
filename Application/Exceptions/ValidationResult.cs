using Flowingly.Domain.DTOs;

namespace Flowingly.Application.Exceptions
{
    public class ValidationResult
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public ExpenseDto? ExpenseDto { get; private set; }

        private ValidationResult(bool success, ExpenseDto? expenseDto, string errorMessage)
        {
            Success = success;
            ExpenseDto = expenseDto;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult CreateSuccess(ExpenseDto expenseDto)
        {
            return new ValidationResult(true, expenseDto, string.Empty);
        }

        public static ValidationResult CreateError(string errorMessage)
        {
            return new ValidationResult(false, null, errorMessage);
        }
    }
}
