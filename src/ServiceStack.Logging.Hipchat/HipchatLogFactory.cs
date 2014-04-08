using System;

namespace ServiceStack.Logging.Hipchat
{
    public class HipchatLogFactory : ILogFactory
    {
        private readonly string _logRoomName;
        private readonly string _hipchatAuthToken;

        public HipchatLogFactory(string logRoomName, string hipchatAuthToken = null)
        {
            _logRoomName = logRoomName;
            _hipchatAuthToken = hipchatAuthToken;
        }
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        public ILog GetLogger(Type type)
        {
            return GetLogger(type.ToString());
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        public ILog GetLogger(string typeName)
        {
            return new HipchatLog(_logRoomName, _hipchatAuthToken);
        }
    }
}
