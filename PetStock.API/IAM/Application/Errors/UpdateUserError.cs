namespace PetStock.API.IAM.Application.Errors;

public enum UpdateUserError
{
    UserNotFound,
    InvalidUserData,
    DuplicateUserEmail,
    UnexpectedError
}