using Newtonsoft.Json;

namespace H3___Rumrejsen.Models
{
    public class CadetKey : ApiKey
    {

        [JsonProperty("renewalPeriod")]
        public int RenewalPeriod { get; private set; }
        [JsonProperty("lastRenewed")]
        public DateTime? LastRenewed { get; private set; }
        [JsonProperty("usesPerPeriod")]
        public int UsesPerPeriod { get; private set; }
        [JsonProperty("usesLeft")]
        public int UsesLeft { get; private set; }

        public override bool IsValid()
        {
            if (LastRenewed == null)
            {
                LastRenewed = DateTime.Now;
            }
            else if (DateTime.Now > LastRenewed.Value.AddMinutes(RenewalPeriod))
            {
                Renew();
            }

            if (UsesLeft <= 0)
            {
                return false;
            }

            if (base.IsValid() == false)
            {
                return false;
            }

            // Subtract this use and validate
            UsesLeft--;
            return true;
        }

        /// <summary>
        /// Renew the uses left on the key if the renewal period has passed
        /// </summary>
        private void Renew()
        {

            UsesLeft = UsesPerPeriod;

        }
    }
}
