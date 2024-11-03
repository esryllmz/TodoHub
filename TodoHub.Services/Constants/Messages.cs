using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Services.Constants
{
    public static class Messages
    {
        public const string TodoAddedMessage = "Todo Eklendi.";
        public const string TodoUpdatedMessage = "Todo Güncellendi.";
        public const string TodoDeletedMessage = "Todo Silindi.";

        public static string TodoIsNotPresentMessage(Guid id)
        {
            return $"İlgili ID'ye göre todo bulunamadı: {id}.";
        }
    }

}
