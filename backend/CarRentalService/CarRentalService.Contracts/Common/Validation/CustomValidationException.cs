namespace CarRentalService.Contracts.Common.Validation
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(List<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
