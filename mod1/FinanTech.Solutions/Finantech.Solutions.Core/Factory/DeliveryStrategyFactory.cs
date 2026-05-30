using Finantech.Solutions.Core.Models.Enums;
using Finantech.Solutions.Core.Strategy;
using Finantech.Solutions.Core.Strategy.DeliveryStrategy;
using Finantech.Solutions.Core.Strategy.Intefaces;

namespace Finantech.Solutions.Core.Factory;

public class DeliveryStrategyFactory
{
    public static IDeliveryStrategy GetStrategy(DeliveryChannel channelType)
    {
        return channelType switch
        {
            DeliveryChannel.Email => new EmailDeliveryStrategy(),
            DeliveryChannel.SharedFolder => new SharedFolderDeliveryStrategy(),
            DeliveryChannel.Api => new ApiDeliveryStrategy(),
            _ => throw new NotImplementedException($"El canal {channelType} no está soportado.")
        };
    }
}
