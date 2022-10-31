using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Payment.Commands;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using PayApp.Tests.Common;
using Xunit;

namespace PayApp.Tests.Payment.Commands
{
    public class PayByNumberTests : TestCommandBase
    {
        [Fact]
        public async Task PayByNumber_Success()
        {
            var handler = new PayByNumberCommandHandler(Context, Localizer);
            var command = new PayByNumberCommand {PhoneNumber = "+77001113322", Sum = 3100};
            var stringEnum = NumberType.Altel.ToString();

            var result = await handler.Handle(command, CancellationToken.None);
            var entity =
                await Context.Payments.FirstOrDefaultAsync(p =>
                    p.PhoneNumber == command.PhoneNumber && p.Sum == command.Sum);

            Assert.Equal(stringEnum, result);
            Assert.Equal(command.PhoneNumber, entity.PhoneNumber);
            Assert.NotNull(entity);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PayByNumber_WrongMobileOperatorException()
        {
            var handler = new PayByNumberCommandHandler(Context, Localizer);
            var command = new PayByNumberCommand {PhoneNumber = "+77021113322", Sum = 2100};

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}