﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infra.Auth.Constants
{
    public static class Constants
    {
        public static class JwtClaimIdentifiers
        {
            public const string Rol = "rol", Id = "id";
        }

        public static class JwtClaims
        {
            public const string ApiAccess = "api_access";
        }

    }
}
