using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TreeApp.ApiService.Model.Dto;
using TreeApp.ApiService.Services.Journal;

namespace TreeApp.ApiService.Controllers;

[ApiController]
public class JournalController : Controller
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }
    
    [HttpPost("api.user.journal.getSingle")]
    public async Task<ActionResult<MJournal>> GetJournal([FromQuery]int id,CancellationToken cancellationToken)
        => Ok(await _journalService.GetJournalByIdAsync(id, cancellationToken));

    [HttpPost("api.user.journal.getRange")]
    public async Task<ActionResult<MRange<MJournalInfo>>> GetRange([FromQuery] int skip, [FromQuery] int take,
        [FromBody] VJournalFilter filter, CancellationToken cancellationToken)
        => Ok(await _journalService.GetJournalRangeAsync(skip, take, filter, cancellationToken));

}