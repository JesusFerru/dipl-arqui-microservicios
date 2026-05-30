using Finantech.Solutions.Core.Models.Enums;

namespace Finantech.Solutions.Core.Strategy.Intefaces;
public interface IDeliveryStrategy
{
    void Deliver(string finalReport, string destination);
}
