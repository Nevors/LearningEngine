﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        readonly IMediator _mediator;
        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{themename}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCards(string themename)
        {
            var query = new GetThemeCardsQuery(themename);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}