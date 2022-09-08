using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Models.Commands
{
    public class UpdateTechnologyModel
    {
        public int ProgramingLanguageId { get; set; }
        public string Name { get; set; }
    }
}
