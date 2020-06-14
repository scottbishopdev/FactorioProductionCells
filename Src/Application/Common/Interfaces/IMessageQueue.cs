using System;

namespace FactorioProductionCells.Application.Common.Interfaces
{
    public interface IMessageQueue
    {
        void SendMessage(String ChannelName, String Message);
    }
}
