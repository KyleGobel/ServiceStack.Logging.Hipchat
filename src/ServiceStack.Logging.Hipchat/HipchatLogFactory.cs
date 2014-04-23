using System;

namespace ServiceStack.Logging.Hipchat
{
    public class HipchatLogFactory : ILogFactory
    {
        private readonly string _logRoomName;
        private readonly string _hipchatAuthId;
        private readonly string _hipchatAuthSecret;
        private readonly string _hipchatAuthToken;
        public HipchatLogFactory(string logRoomName, string hipchatAuthToken = null)
        {
            _logRoomName = logRoomName;
            _hipchatAuthToken = hipchatAuthToken;
        }

        public HipchatLogFactory(string logRoomName, string hipchatAuthId, string hipchatAuthSecret)
        {
            _logRoomName = logRoomName;
            _hipchatAuthId = hipchatAuthId;
            _hipchatAuthSecret = hipchatAuthSecret;
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
        /// <param name="typeName">Name of the type/application.</param>
        public ILog GetLogger(string typeName)
        {
            HipchatLog logger;
            if (!_hipchatAuthId.IsEmpty() && !_hipchatAuthSecret.IsEmpty())
            {
                
                logger =  new HipchatLog(_logRoomName, _hipchatAuthId, _hipchatAuthSecret);
            }
            else
            {
                logger = new HipchatLog(_logRoomName, _hipchatAuthToken);
            }
            logger.ApplicationName = typeName;

            return logger;
        }
    }
}
