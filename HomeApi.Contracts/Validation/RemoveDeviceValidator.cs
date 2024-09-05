using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    internal class RemoveDeviceValidator : AbstractValidator<RemoveDeviceRequest>
    {
        public RemoveDeviceValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

        }

    }
}
