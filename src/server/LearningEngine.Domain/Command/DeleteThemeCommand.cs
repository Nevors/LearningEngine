﻿using FluentValidation;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class DeleteThemeCommand : IRequest, IPipelinePermissionCommand
    {
        public DeleteThemeCommand(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }

        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;
    }

    public class DeleteThemeCommandValidator : AbstractValidator<DeleteThemeCommand>
    {
        public DeleteThemeCommandValidator()
        {
            RuleFor(theme => theme.ThemeId).GreaterThan(0);
            RuleFor(theme => theme.UserId).GreaterThan(0);
        }
    }
}
