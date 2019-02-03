namespace Provausio.Common
{
    /// <summary>
    /// Enum values are aligned to PKs in Surveys.dbo.DeliveryMethod
    /// </summary>
    public enum DeliveryMethod
    {
        /// <summary>
        /// The value was not set
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Send via email
        /// </summary>
        Email = 1,

        /// <summary>
        /// Send via text message
        /// </summary>
        Sms = 2,

        /// <summary>
        /// Send via external service
        /// </summary>
        Service = 3,

        /// <summary>
        /// Mobile push notifcation
        /// </summary>
        Push = 4,

        /// <summary>
        /// Multimedia message (e.g. photo, video, etc)
        /// </summary>
        Mms = 5
    }
}
