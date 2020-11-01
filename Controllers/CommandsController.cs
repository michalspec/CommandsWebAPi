using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFirstYTry.Data;
using ApiFirstYTry.Dtos;
using ApiFirstYTry.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirstYTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>>GetAllCommands()
        {
            var commands = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name ="GetCommandById")]
        public ActionResult<CommandReadDto>GetCommandById(int id)
        {
            var command = _repo.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto>CreateCommand(CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(command);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute("GetCommandById", new { Id = commandReadDto.Id }, commandReadDto);
            //return Ok(commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commands = _repo.GetCommandById(id);
            if (commands == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commands);

            _repo.UpdateCommand(commands);

            _repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommendUpdate(int id, JsonPatchDocument<CommandUpdateDto>patchDocument)
        {
            var command = _repo.GetCommandById(id);
            if(command == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(command);

            patchDocument.ApplyTo(commandToPatch, ModelState);
            
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);

            _repo.UpdateCommand(command);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = _repo.GetCommandById(id);
            if(command == null)
            {
                return NotFound();
            }

            _repo.DeleteCommand(command);

            _repo.SaveChanges();

            return NoContent();
        }
    }
}
