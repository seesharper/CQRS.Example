using System;
using System.Net.Mail;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace CQRS.Example.Application.Tests.Customizations
{
    public class MailAddressCustomization : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is PropertyInfo property)
            {
                if (property.Name.Contains("email", StringComparison.InvariantCultureIgnoreCase))
                {
                    return context.Create<MailAddress>().Address;
                }
                else
                {
                    return new NoSpecimen();
                }

            }

            return new NoSpecimen();
        }
    }
}