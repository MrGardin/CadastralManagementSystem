using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProposalsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProposalsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitProposal(Proposal proposal)
    {
        _context.Proposals.Add(proposal);
        await _context.SaveChangesAsync();
        return Ok("Предложение отправлено");
    }

    [HttpGet]
    public async Task<IActionResult> GetProposals()
    {
        var proposals = await _context.Proposals.ToListAsync();
        return Ok(proposals);
    }

    [HttpPost("approve/{id}")]
    public async Task<IActionResult> ApproveProposal(int id)
    {
        var proposal = await _context.Proposals.FindAsync(id);
        if (proposal == null) return NotFound("Предложение не найдено");

        var property = new Property
        {
            Address = proposal.Address,
            CadastralNumber = proposal.CadastralNumber,
            Description = proposal.Description,
            Area = proposal.Area,
            Owner = proposal.Owner
        };

        _context.Proposals.Remove(proposal);
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return Ok("Предложение добавлено в базу");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProposal(int id)
    {
        var proposal = await _context.Proposals.FindAsync(id);
        if (proposal == null) return NotFound("Предложение не найдено");

        _context.Proposals.Remove(proposal);
        await _context.SaveChangesAsync();
        return Ok("Предложение удалено");
    }
}
