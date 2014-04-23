using System;
using System.Collections.Generic;
using System.Text;
using HipchatApiV2;
using HipchatApiV2.Enums;

namespace ServiceStack.Logging.Hipchat
{
    public class HipchatLog : ILog
    {
        private readonly string _roomName;
        private readonly string _hipchatAuthId;
        private readonly string _hipchatAuthSecret;
        private readonly string _hipchatAuthToken;
        private const string NEW_LINE = "<br/>";
        public string ApplicationName;

        public HipchatLog(string roomName, string hipchatAuthToken = null)
        {
            _roomName = roomName;
            _hipchatAuthToken = hipchatAuthToken;
        }

        public HipchatLog(string roomName, string hipchatAuthId, string hipchatAuthSecret)
        {
            _roomName = roomName;
            _hipchatAuthId = hipchatAuthId;
            _hipchatAuthSecret = hipchatAuthSecret;
        }

        private void Write(object message, Exception exception, RoomColors color)
        {
            var sb = new StringBuilder();

            if (ApplicationName != null)
            {
                sb.Append("<b>Application: </b>" + ApplicationName).Append(NEW_LINE);
            }
            sb.Append(message).Append(NEW_LINE);

            while (exception != null)
            {
                sb.Append("Message: ").Append(exception.Message).Append(NEW_LINE)
                    .Append("Source: ").Append(exception.Source).Append(NEW_LINE)
                    .Append("Target site: ").Append(exception.TargetSite).Append(NEW_LINE)
                    .Append("Stack trace: ").Append(exception.StackTrace).Append(NEW_LINE);

                exception = exception.InnerException;
            }

            var hipchatClient = new HipchatClient(_hipchatAuthToken);

            if (_hipchatAuthSecret != null && _hipchatAuthSecret != null)
            {
                var token = hipchatClient.GenerateToken(GrantType.ClientCredentials,
                new List<TokenScope>
                {
                    TokenScope.SendNotification,
                },
                _hipchatAuthId,/*Auth Id*/
                _hipchatAuthSecret /*Auth Secret*/);

                HipchatApiConfig.AuthToken = token.Access_Token;
                hipchatClient.SendNotification(_roomName, sb.ToString(), color);
            }
            else
            {
                hipchatClient.SendNotification(_roomName, sb.ToString(), color);
            }
               
        }
        public void Debug(object message)
        {
            Write("<b>DEBUG: </b>" + message,null, RoomColors.Purple);
        }

        public void Debug(object message, Exception exception)
        {
            Write("<b>DEBUG: </b>" +  message, exception, RoomColors.Purple);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Write("<b>DEBUG: </b>" + string.Format(format, args), null,RoomColors.Purple);
        }

        public void Error(object message)
        {
            Write("<b>ERROR: </b>" + message,null, RoomColors.Red);
        }

        public void Error(object message, Exception exception)
        {
             Write("<b>ERROR: </b>" +  message,exception, RoomColors.Red);
        }

        public void ErrorFormat(string format, params object[] args)
        {
             Write("<b>ERROR: </b>" + string.Format(format, args),null, RoomColors.Red);
        }

        public void Fatal(object message)
        {
            Write("<b>FATAL: </b>" + message,null, RoomColors.Red);
        }

        public void Fatal(object message, Exception exception)
        {
            Write("<b>FATAL: </b>" + message,exception, RoomColors.Red);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Write("<b>FATAL: </b>" + string.Format(format, args),null, RoomColors.Red);
        }

        public void Info(object message)
        {
            Write("<b>INFO: </b>" + message,null, RoomColors.Green);
        }

        public void Info(object message, Exception exception)
        {
            Write("<b>INFO: </b>" + message , exception, RoomColors.Green);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Write("<b>INFO: <b/>" + string.Format(format, args),null, RoomColors.Green);
        }

        public void Warn(object message)
        {
            Write("<b>WARN: </b>" + message,null, RoomColors.Gray);
        }

        public void Warn(object message, Exception exception)
        {
            Write("<b>WARN: </b>" + message ,exception, RoomColors.Gray);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Write("<b>WARN: </b>" + string.Format(format, args),null, RoomColors.Gray);
        }

        public bool IsDebugEnabled { get { return true; } }
    }
}