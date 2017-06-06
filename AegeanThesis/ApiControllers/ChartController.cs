using AegeanThesis.Models;
using System.Linq;
using System.Web.Http;

namespace AegeanThesis.ApiControllers
{
    public class ChartController : ApiController
    {
        [HttpGet]
        public object Get(int ID = -1)
        {
            if (ID == -1)
                return null;



            using (ApplicationDbContext con = new ApplicationDbContext())
            {
                var t = con.Thesises.Find(ID);

                return t.Progresses.Select(x=>new { x.ID, x.Name, x.StartDate, x.EndDate, x.ProgressPercentage, x.Dependencies });
            }
        }
        [HttpPost]
        public void Post()
        {

        }
    }
}
