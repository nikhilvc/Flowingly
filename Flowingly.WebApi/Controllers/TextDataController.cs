using Microsoft.AspNetCore.Mvc;
using Flowingly.Application.Interfaces;
using Flowingly.Application.Exceptions;
using System.Text.RegularExpressions;

namespace Flowingly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextDataController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public TextDataController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public ActionResult<ValidationResult> ExtractExpense(string message)
        {
            try
            {
                var validationResult = ValidateXml(message);
                if (validationResult != null) { return validationResult; };
                return _expenseService.ExtractExpense(message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static ValidationResult? ValidateXml(string message)
        {
            var error = IsAllTagsHaveClosing(message);
            if (!string.IsNullOrEmpty(error))
            {
                return ValidationResult.CreateError($"Opening tag {error} with no corresponding closing tags.");
            }

            if (IsTotalExists(message))
            {
                return ValidationResult.CreateError("Missing <total> element.");
            }

            return null;

        }

        public static string IsAllTagsHaveClosing(string text)
        {
            var tagStack = new Stack<string>();

            // Regular expression to match XML tags (opening or closing)
            var tagPattern = @"<\/?(\w+)[^>]*>";

            var matches = System.Text.RegularExpressions.Regex.Matches(text, tagPattern);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var tag = match.Groups[1].Value;
                if (match.Value.StartsWith("</"))
                {
                   
                    if (tagStack.Count == 0 || tagStack.Peek() != tag)
                    {
                        return tagStack.Peek();
                    }
                    tagStack.Pop();
                }
                else
                {
                    tagStack.Push(tag);
                }
            }

            // If the tagStack is empty, all opening tags have corresponding closing tags
            return tagStack.Count == 0 ? string.Empty : tagStack.Peek();
        }

        public static bool IsTotalExists(string text)
        {
            return !text.Contains("<total>");
        }

    }
}
