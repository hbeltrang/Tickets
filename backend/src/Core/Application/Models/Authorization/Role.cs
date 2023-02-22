namespace Tickets.Application.Models.Authorization
{
    public static class Role
    {
        public const string ADMIN = nameof(ADMIN);
        public const string APIADMIN = nameof(APIADMIN);        
        public const string USER = nameof(USER);
        public const string PROMOTER = nameof(PROMOTER);
        public const string ADMINOrAPIADMIN = ADMIN + "," + APIADMIN;
        public const string ADMINOrAPIADMINOrUser = ADMIN + "," + APIADMIN + "," + USER;
    }
}
