using MarriageAPI.Data;
using MarriageAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MarriageAPI.Services
{
    public class WeddingService
    {
        private readonly ApplicationDbContext _appDbContext;

        public WeddingService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> GetMarried(int person1Id, int person2Id)
        {
            try
            {
                // prevents same person to marry himself
                if (person1Id != person2Id)
                {
                    // get's single persons by id
                    Person p1 = _appDbContext.People.Where(p => p.Id == person1Id)
                                                    .Where(p => p.Married == false)
                                                    .SingleOrDefault();

                    Person p2 = _appDbContext.People.Where(p => p.Id == person2Id)
                                                    .Where(p => p.Married == false)
                                                    .SingleOrDefault();

                    if (p1 != null && p2 != null)
                    {
                        // congratulations!
                        Marriage marriage = new(p1, p2);

                        _appDbContext.Marriages.Add(marriage);
                        await _appDbContext.SaveChangesAsync();

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetDivorced(int id)
        {
            try
            {
                Marriage marriage = _appDbContext.Marriages.Include(m => m.Person1)
                                                           .Include(m => m.Person2)
                                                           .Where(m => m.Id == id)
                                                           .FirstOrDefault();

                if (marriage != null)
                {
                    // congratulations!
                    marriage.Divorce();

                    _appDbContext.People.Update(marriage.Person1);
                    _appDbContext.People.Update(marriage.Person2);

                    _appDbContext.Marriages.Remove(marriage);

                    await _appDbContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
