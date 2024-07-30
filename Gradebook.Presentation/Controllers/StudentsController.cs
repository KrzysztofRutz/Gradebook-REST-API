using Gradebook.Application.Commands.Grades.GiveStudentGrade;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Application.Commands.Students.RemoveStudent;
using Gradebook.Application.Commands.Students.UpdateStudent;
using Gradebook.Application.Dtos;
using Gradebook.Application.Queries.Students.GetStudentByEmail;
using Gradebook.Application.Queries.Students.GetStudentById;
using Gradebook.Application.Queries.Students.GetStudents;
using Gradebook.Application.Queries.Students.GetStudentsWithDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Gradebook.Presentation.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : Controller
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get students")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetStudentsQuery());
        return Ok(result);
    }

    [HttpGet("[action]")]
    [SwaggerOperation("Get students with details")]
    [ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetWithDetails()
    {
        var result = await _mediator.Send(new GetStudentsWithDetailsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get student by ID")]
    [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetById([FromRoute]int id)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("[action]/{email}")]
    [SwaggerOperation("Get student by email")]
    [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetByEmail([FromRoute]string email)
    {
        var result = await _mediator.Send(new GetStudentByEmailQuery(email));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [SwaggerOperation("Add student")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody]AddStudentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [SwaggerOperation("Update student")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Put([FromBody]UpdateStudentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/grades/{gradeId}")]
    [SwaggerOperation("Give student grade")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> GiveGrade([FromRoute] int id, [FromRoute] int gradeId)
    {
        await _mediator.Send(new GiveStudentGradeCommand(id, gradeId));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove student")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromRoute]int id)
    {
        await _mediator.Send(new RemoveStudentCommand(id));
        return NoContent();
    }
}
