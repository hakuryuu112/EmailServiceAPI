using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailServiceAPI.Controllers.KPIs
{
    [Route("api/kpi")]
    [ApiController]
    public class KPIController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;

        public KPIController(DBContext.DatabaseContext context)
        {
            _context = context;
        }

        [Route("kpi-summary/{year}")]
        [HttpGet]
        public async Task<IActionResult> GetKpiSummary([FromRoute] int year)
        {
            var summaryQuery = _context.Companies
                .Select(c => new
                {
                    CompanyId = c.Id,
                    CompanyName = c.Name,
                    AverageKpiScore = _context.KPIs
                        .Where(k => k.CompanyId == c.Id && k.MeasuredAt.Year == year)
                        .Average(k => (double?)k.Score) ?? 0
                });

            var summary = await summaryQuery.ToListAsync();

            return Ok(summary);
        }
    }
}
