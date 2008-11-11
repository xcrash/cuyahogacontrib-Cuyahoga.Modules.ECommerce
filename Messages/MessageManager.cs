using System;
using Igentics.Common.Util;

namespace Igentics.Common.ECommerce.Messages {

    public interface IIdentityGenerator {
        string GenerateID();
    }

    public class MessageManager : IIdentityGenerator {

        public const int MESSAGE_COUNT_MULTIPLIER = 1000;
        private const float MAX_MESSAGES_PER_SECOND = 1e9f;

        public string GenerateID() {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss-") + StringUtils.GenerateRandomText(GetMessageIdLength(MAX_MESSAGES_PER_SECOND), StringUtils.CAR_PLATE_VALUES);
            //return DateTime.Now.ToString("yyyyMMdd-") + System.Guid.NewGuid().ToString().ToUpper(); // StringUtils.GenerateRandomText(GetMessageIdLength(maxMessages));
        }

        //Calculates the number of characters to cover the maximum number of unique messages
        //plus a multiplier to avoid ID conflicts
        private int GetMessageIdLength(float maxMessages) {
            return (int) Math.Ceiling(Math.Log(maxMessages * MESSAGE_COUNT_MULTIPLIER) / Math.Log(StringUtils.CAR_PLATE_VALUES.Length));
        }
    }
}
