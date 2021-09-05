using System;
using System.Collections.Generic;
using System.Text;
using AK.Application.DTOs;

namespace AK.Application.Commands.Drug
{
    public class DrugCreateCommand : DrugCommandBase<DrugDto>
    {
        public DrugCreateCommand(DrugDto model) : base(model)
        {
        }

        public DrugCreateCommand()
        {
            
        }
    }
}
