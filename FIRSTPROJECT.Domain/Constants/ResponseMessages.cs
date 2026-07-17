namespace FIRSTPROJECT.Domain.Constants;
//All The messages that are used in the application are defined here. This is to avoid hardcoding the messages in the code and to have a single place to manage them. This is a good practice to follow as it makes it easier to manage the messages and also makes it easier to support multiple languages in the future if needed.
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
    public const string TicketNotFound = "Ticket not found.";
}