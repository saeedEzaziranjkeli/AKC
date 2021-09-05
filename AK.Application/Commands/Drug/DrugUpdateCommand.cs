using System;
using System.Collections.Generic;
using System.Text;
using AK.Application.DTOs;

namespace AK.Application.Commands.Drug
{
    public class DrugUpdateCommand : DrugCommandBase<DrugDto>
    {
        public DrugUpdateCommand(DrugDto model) : base(model)
        {

        }

        public DrugUpdateCommand()
        {
            
        }
    }
}
