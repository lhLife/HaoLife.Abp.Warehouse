using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace HaoLife.Abp.Warehouse.Pages;

public class IndexModel : WarehousePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
