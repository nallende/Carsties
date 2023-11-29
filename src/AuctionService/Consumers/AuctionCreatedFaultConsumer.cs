using System.Data;
using System.Diagnostics.Contracts;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using System;
using System.Collections.Concurrent;
using MassTransit;
using Contracts;

namespace AuctionService;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        System.Console.WriteLine("--> consuming faulty creation");
        
        var exception = context.Message.Exceptions.First();

       
         if   (exception.ExceptionType == "Sytem.ArgumentExceptopn")
         {
            context.Message.Message.Model = "FooBar";
            await context.Publish(context.Message.Message);
         }else
         {
            System.Console.WriteLine("Not an argument exception - update error dashboard somewhere");
         }

        
     }

    Task IConsumer<Fault<AuctionCreated>>.Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        throw new NotImplementedException();
    }
}