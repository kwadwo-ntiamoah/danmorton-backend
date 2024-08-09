using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Jemma.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class User {
            public static Error Conflict => Error.Conflict(code: "User.Conflict", description: "User already exists"); 
            public static Error NotFound => Error.NotFound(code: "User.NotFound", description: "User not found"); 
            public static Error InvalidLogin => Error.Unauthorized(code: "Auth.InvalidLogin", description: "Invalid login credentials");
            public static Error CreateError => Error.Failure(code: "Auth.CreateError", description: "An error occurred adding user. Contact Administrator if this error persists");
            public static Error UpdateError => Error.Failure(code: "Auth.UpdateError", description: "An error occurred updating user details");
        }
    }
}