namespace FIRSTPROJECT.Domain.Constants;

public static class ResponseMessages
{
    // Authentication
    public const string EmailAlreadyExists = "Email already exists.";
    public const string InvalidCredentials = "Invalid email or password.";

    // Category
    public const string CategoryNotFound = "Category not found.";
    public const string CategoryAlreadyExists = "Category already exists.";

    // Generic / unexpected
    public const string UnexpectedError = "An unexpected error occurred. Please try again later.";
}