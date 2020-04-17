﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.Extensions
{
    public static class GetUserIdFromJwtTokenExtension
    {
        private const int _userIdPosition = 1;
        public static int GetUserId(this JwtSecurityToken jwtSecurityToken)
        {
            int result;
            if(!int.TryParse(jwtSecurityToken.Claims.ElementAt(_userIdPosition).Value, out result))
            {
                throw new Exception("Invalid id in user claims");
            }

            return result;
        }
    }
}