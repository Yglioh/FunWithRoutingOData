using System.Collections.Generic;

namespace Repositories.V1
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> Get();
        T GetById(int id);
    }
}
