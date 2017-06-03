using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Models
{
    public partial class Skills
    {
        public Skills(AddSkillViewModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Cost = model.Cost;
            SkillId = Guid.NewGuid();
        }
    }
}