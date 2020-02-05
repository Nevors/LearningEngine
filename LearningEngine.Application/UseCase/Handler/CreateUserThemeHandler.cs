﻿using LearningEngine.Application.UseCase.Command;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class CreateUserThemeHandler : BaseUseCaseHandler<Unit, CreateUserThemeCommand>
    {
        private readonly IMediator _mediator;
        public CreateUserThemeHandler(IMediator mediator, ITransactionUnitOfWork uow): base(uow)
        {
            _mediator = mediator;
        }

        protected override async Task<Unit> Action(CreateUserThemeCommand request)
        {
            var getUserByNameQuery = new GetUserByNameQuery(request.UserName);
            var user = await _mediator.Send(getUserByNameQuery);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var createThemeCommand = new CreateThemeCommand(request.ThemeName, request.Description, request.IsPublic, request.ParentThemeId);
            await _mediator.Send(createThemeCommand);
            var linkCommand = new LinkUserToThemeCommand(request.UserName, request.ThemeName, TypeAccess.Read | TypeAccess.Write);
            await _mediator.Send(linkCommand);

            return default;

        }
    }
}