using Models.V1;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.V1
{
    public interface IParameterRepository : IRepository<Parameter> { }

    public class ParameterRepository : IParameterRepository
    {
        public ICollection<Parameter> Get()
        {
            var parameters = new List<Parameter>
            {
                new TextParameter { Id = 1, Label = "Text parameter", DefaultValue = "Hello" },
                new NumberParameter { Id = 2, Label = "Text parameter", DefaultValue = 42 },
                new CheckBoxParameter { Id = 3, Label = "Text parameter", DefaultValue = true },
                new DateParameter { Id = 4, Label = "Text parameter", DefaultValue = System.DateTimeOffset.Now.AddDays(1) }
            };

            return parameters;
        }

        public Parameter GetById(int id)
        {
            return Get().FirstOrDefault(parameter => parameter.Id.Equals(id));
        }
    }
}
