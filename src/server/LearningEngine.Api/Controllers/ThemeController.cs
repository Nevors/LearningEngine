﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningEngine.Api.ViewModels;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Query;
using LearningEngine.Application.UseCase.Command;
using System.Net.WebSockets;
using LearningEngine.Api.Authorization;
using LearningEngine.Api.Extensions;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.DTO;
using LearningEngine.Application.UseCase.Query;
using LearningEngine.Api.AppFilters;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtTokenCryptographer workWithJwtToken;

        public ThemeController(IMediator mediator, IJwtTokenCryptographer workWithJwtToken)
        {
            this.mediator = mediator;
            this.workWithJwtToken = workWithJwtToken;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserTheme([FromForm] CreateThemeViewModel vm)
        {
            var command = new CreateUserThemeCommand(this.GetUserName(), 
                                                     vm.ThemeName, 
                                                     vm.Description,
                                                     vm.IsPublic, 
                                                     this.GetUserId(), 
                                                     vm.ParentThemeId);

            await mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{themeId}")]
        public async Task<IActionResult> DeleteTheme([FromRoute] int themeId)
        {
            var command = new DeleteThemeCommand(themeId, this.GetUserId());

            await mediator.Send(command);

            return Ok();
        }

        [HttpGet("{themeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
        {
            var query = new GetThemeHeaderQuery(themeId, this.GetUserId());

            var themes = await mediator.Send(query);

            return Ok(themes);
        }

        [HttpGet("{themeId}/fullInfo")]
        public async Task<IActionResult> GetFullInfo([FromRoute] int themeId)
        {
            var query = new GetThemeFullInfoQuery(this.GetUserId(), themeId);

            var result = await mediator.Send(query);

            return Ok(new { theme = result, isRoot = false });
        }

        [HttpGet("userRootThemes")]
        public async Task<IActionResult> GetUserRootThemes()
        {
            var query = new GetRootThemesByUserIdQuery(this.GetUserId());

            var rootThemes = await mediator.Send(query);

            return Ok(rootThemes);
        }

        [HttpPost("linkUserToTheme")]
        public async Task<IActionResult> LinkUserToTheme([FromForm] int themeId, [FromForm] TypeAccess typeAccess)
        {
            var command = new LinkThemeAndAllSubThemesToUserCommand(this.GetUserId(), themeId, typeAccess);

            await mediator.Send(command);

            return Ok();
        }

        [HttpPut("{themeId}")]
        public async Task<IActionResult> EditTheme([FromForm] ThemeDto themeDto, [FromRoute] int themeId)
        {
            var command = new EditThemeCommand(themeDto, this.GetUserId(), themeId);

            await mediator.Send(command);

            return Ok();
        }
    }
}