﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFirstYTry.Data;
using ApiFirstYTry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirstYTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;

        public CommandsController(ICommanderRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>>GetAllCommands()
        {
            var commands = _repo.GetAppCommands();

            return Ok(commands);
        }

        [HttpGet("{id}")]
        public ActionResult<Command>GetCommandById(int id)
        {
            var command = _repo.GetCommandById(id);

            return command;
        }

    }
}
