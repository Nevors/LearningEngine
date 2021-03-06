﻿using FluentValidation;
using LearningEngine.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class CheckUserThemePermissionsQuery : IRequest
    {
        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public TypeAccess Access { get; private set; }

        public CheckUserThemePermissionsQuery(int userId, int themeId, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }

    public class CheckUserThemePermissionsQueryValidator : AbstractValidator<CheckUserThemePermissionsQuery>
    {
        public CheckUserThemePermissionsQueryValidator()
        {
            RuleFor(permission => permission.UserId).GreaterThan(0);
            RuleFor(permission => permission.ThemeId).GreaterThan(0);
        }
    }
}
