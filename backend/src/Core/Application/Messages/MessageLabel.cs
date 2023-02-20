namespace Tickets.Application.Messages
{
    public class MessageLabel
    {
        public const string UserNotFound = "User not found";
        public const string UserEmailNotFound = "Email not found";
        public const string UserNotAutenticated = "User not autenticated";
        public const string UserNotBlocked = "User is blocked, contact admin";
        public const string UserCredentialsError = "User credentials with errors";
        public const string UserAlreadyExists = "The user email already exists";
        public const string UserRegisterError = "Error to register the user";
        public const string UserWrongPassword = "The current password is wrong";
        public const string UserResetPasswordError = "Error to reset password";
        public const string UserPasswordSuccess = "Password change was successfully";
        public const string UserPasswordConfirmDifferent = "The password and confirmation are different";
        public const string UserResetPasswordSendEmail = "The email was sent to";
        public const string UserResetPasswordEmailError = "Error to send email";
        public const string UserResetPasswordEmailBody = "Reset password, click here";
        public const string UserResetPasswordEmailSubject = "Change password";
        public const string UserChangeStatusError = "Can't change user status";
        public const string UserUpdateError = "Failed to update user";
        public const string UserAssignRoleError = "Assigned role does not exist";
        
        public const string ImageSaveError = "Error to save image";

        public const string ErrorTransaction = "Error in transaction";


        public const string ValidatorEmailEmpty = "Email cann't be empty";
        public const string ValidatorPasswordEmpty = "Password cann't be empty";
        public const string ValidatorNameEmpty = "Name cann't be empty";
        public const string ValidatorLastNameEmpty = "LastName cann't be empty";
        public const string ValidatorPhoneEmpty = "Phone cann't be empty";


    }
}
