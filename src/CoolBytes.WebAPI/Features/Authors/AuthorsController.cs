﻿using CoolBytes.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CoolBytes.WebAPI.Features.Authors.CQ;
using CoolBytes.WebAPI.Features.Authors.ViewModels;

namespace CoolBytes.WebAPI.Features.Authors
{
    [ApiController]
    [Authorize("admin")]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AuthorViewModel>> Get(bool includeProfile)
        {
            var query = new GetAuthorQuery() { IncludeProfile = includeProfile };

            return this.OkOrNotFound(await _mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(AddAuthorCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Operation not allowed, only one author can be created per user.");
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Update(UpdateAuthorCommand command)
            => Ok(await _mediator.Send(command));
    }
}