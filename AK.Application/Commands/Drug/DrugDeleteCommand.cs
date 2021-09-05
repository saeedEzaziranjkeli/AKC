using System;
using System.Collections.Generic;
using System.Text;
using AK.Application.DTOs;

namespace AK.Application.Commands.Drug
{
    public class DrugDeleteCommand : DrugCommandBase<bool>
    {
        public DrugDeleteCommand(DrugDto model) : base(model)
        {
            
        }

        public DrugDeleteCommand()
        {
            
        }
    }
}
