using MarriageAPI.Data;
using MarriageAPI.Models;
using System.Threading.Tasks;

namespace MarriageAPI.Services
{
    public class PeopleService
    {
        private readonly ApplicationDbContext _appDbContext;

        public PeopleService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Person GetPerson(int id)
        {
            Person person = _appDbContext.People.Find(id);
           
            if (person != null)
            {
                return person;
            }

            return null;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                _appDbContext.People.Add(person);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                Person person = _appDbContext.People.Find(id);

                _appDbContext.People.Remove(person);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}