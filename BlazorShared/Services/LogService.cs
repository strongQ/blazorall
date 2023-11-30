using GeneralCommon.Models.SignalR;
using GeneralCommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Services
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService : ILogService
    {
        private List<string> _logs = new List<string>();

        // 日志事件
        public event EventHandler<string> AddLogEvent;

        public void Log(string info)
        {
            var log = $"{DateTime.Now} Info：{info} ";
            AddLogs(log);
        }

        private void AddLogs(string log)
        {
            AddLogEvent?.Invoke(this, log);
            _logs.Add(log);
            if (_logs.Count > 100)
            {
                _logs.RemoveAt(0);
            }
        }

        public void LogError(string info, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogError(string error)
        {
            var log = $"{DateTime.Now} error：{error} ";
            AddLogs(log);
        }

        public List<string> GetLogs()
        {
            return _logs.TakeLast(50).ToList();
        }

        public void LogHeart(string info)
        {
            var log = $"{DateTime.Now} heart：{info} ";
            AddLogs(log);
        }

        public void LogRemote(RemoteLog remoteLog)
        {
            throw new NotImplementedException();
        }

        Task ILogService.LogRemote(RemoteLog remoteLog)
        {
            throw new NotImplementedException();
        }
    }
}
