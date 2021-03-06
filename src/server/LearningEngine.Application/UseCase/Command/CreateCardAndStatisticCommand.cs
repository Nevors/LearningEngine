﻿using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Application.UseCase.Command
{
    public class CreateCardAndStatisticCommand : IRequest, IPipelinePermissionQuery
    {
        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public int ObjectId => ThemeId;

        public string Question { get; private set; }

        public string Answer { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public CreateCardAndStatisticCommand(int userId, int themeId, string question, string answer)
        {
            UserId = userId;
            ThemeId = themeId;
            Question = question;
            Answer = answer;
        }
    }
}
