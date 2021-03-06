﻿using LearningEngine.Application.PipelineValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Domain.Interfaces;
using System.Reflection;
using LearningEngine.Application.UseCase.Query;

namespace LearningEngine.Api.Extensions
{
    public static class GetUserIdFromJwtTokenExtension
    {
        private const int UserIdPosition = 1;
        private const int UserNamePosition = 0;

        public static int GetUserId(this ControllerBase httpContext)
        {
            if (int.TryParse(httpContext.User.Claims.ElementAt(UserIdPosition).Value, out int result))
            {
                return result;
            }

            throw new Exception("Invalid id in user claims");
        }

        public static string GetUserName(this ControllerBase httpContext)
        {
            return httpContext.User.Claims.ElementAt(UserNamePosition).Value;
        }
    }
}
