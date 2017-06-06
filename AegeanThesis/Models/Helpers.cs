using Microsoft.AspNet.Identity;
using System.Security.Principal;
namespace AegeanThesis.Models
{
    public static class Helpers
    {
        public static ApplicationUser GetCurrentUser(IPrincipal u)
        {

            using (var db = new ApplicationDbContext())
            {
                return db.Users.Find(u.Identity.GetUserId());
            }

        }
    }
}