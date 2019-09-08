using System;

namespace SimpleApi.Core
{
    public class OperateResult
    {
        /// <summary>
        /// 请求状态 Error = 0, Succeed = 1
        /// </summary>
        public ResultStatus Status { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(ResultStatus status, string msg)
        {
            Status = status;
            Msg = msg;
        }

        public OperateResult(ResultStatus status, string msg, object data)
        {
            Status = status;
            Msg = msg;
            Data = data;
        }

        public static OperateResult Succeed(string msg = "ok")
        {
            return new OperateResult(ResultStatus.Succeed, msg);
        }
        public static OperateResult Succeed(string msg, object data)
        {
            return new OperateResult(ResultStatus.Succeed, msg, data);
        }

        public static OperateResult Error(string msg)
        {
            return new OperateResult(ResultStatus.Error, msg);
        }

        public static OperateResult Error(string msg, object data)
        {
            return new OperateResult(ResultStatus.Error, msg, data);
        }
    }
}
