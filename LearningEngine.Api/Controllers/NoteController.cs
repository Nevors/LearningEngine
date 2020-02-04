﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{themename}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotes(string themename)
        {
            var query = new GetThemeNotesQuery(themename);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("{themename}/note")]
        public async Task<IActionResult> AddNote([FromForm]string themeName, [FromForm]string title, [FromForm]string content)
        {
            var command = new CreateNoteCommand(themeName, title, content);

            var result = await _mediator.Send(command);

            return Ok();
        }
    }
}