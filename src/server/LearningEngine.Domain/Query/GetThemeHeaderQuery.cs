﻿using FluentValidation;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>, IPipelinePermissionQuery
    {
        public GetThemeHeaderQuery(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }

        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;
    }

    public class GetThemeHeaderQueryValidator : AbstractValidator<GetThemeHeaderQuery>
    {
        public GetThemeHeaderQueryValidator()
        {
            RuleFor(theme => theme.ThemeId).GreaterThan(0);
            RuleFor(theme => theme.UserId).GreaterThan(0);
        }
    }
}
