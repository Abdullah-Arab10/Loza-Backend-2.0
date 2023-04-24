namespace Loza.API
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/[controller]";

        public const string AuthRoute = "/[controller]";


        public class Auth
        {
            public const string Login = "Login";
            public const string Register = "Register";
        }
        public class User
        {
            public const string GetAllUser = "GetAllUser";
            public const string GetUserById = "{id}";
            public const string UpdateUser= "Update";
            public const string DeleteUser = "DeleteUser";
            public const string CreateUser = "CreateUser";
        }

    }
}
