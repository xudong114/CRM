using Ingenious.Domain.Models;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IDictionaryService : IApplication<DictionaryDTO>
    {
        DictionaryDTO GetByCode(string code);
        DictionaryDTO GetByName(string name);
        DictionaryDTOList GetAll(string category = "");
        List<string> GetCategories();
    }
}
