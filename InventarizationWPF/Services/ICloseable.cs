using System;

namespace InventarizationWPF.Services
{
    public interface ICloseable
    {
        event EventHandler CloseRequest;
    }
}
