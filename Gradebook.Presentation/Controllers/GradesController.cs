using Gradebook.Application.Commands.Grades.AddGrade;
using Gradebook.Application.Commands.Grades.RemoveGrade;
using Gradebook.Application.Dtos;
using Gradebook.Application.Queries.Grades.GetGrades;
using Gradebook.Application.Queries.Grades.GetGradeStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Gradebook.Presentation.Controllers;

[Route("api/grades")]
[ApiController]
public class GradesController : Controller
{
    private readonly IMediator _mediator;

    public GradesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [SwaggerOperation("Get grades.")]
    [ProducesResponseType(typeof(IEnumerable<GradeDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetGradesQuery());
        return Ok(result);
    }

    [HttpGet("{id}/students")]
    [SwaggerOperation("Get grade students")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>),(int)HttpStatusCode.OK )]
    public async Task<ActionResult> GetGradeStudents([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetGradeStudentsQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation("Add grade")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] AddGradeCommand command)
    {
        var grade = await _mediator.Send(command);
        return Created($"api/grades/{grade.Id}", grade);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove grade")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveGradeCommand(id));
        return NoContent();
    }
}
