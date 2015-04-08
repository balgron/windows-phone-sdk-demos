using AVOSCloud.RealtimeMessageV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVOSCloud;

namespace LeanCloud.LeanMeaasge.Demo.Security
{
    /// <summary>
    /// 签名示例类，推荐开发者用这段代码理解签名的整体概念，正式生产环境，请慎用
    /// </summary>
    public class SampleSignatureFactory : ISignatureFactoryV2
    {
        /// <summary>
        /// 为更新对话成员的操作进行签名
        /// </summary>
        /// <param name="conversationId">对话的Id</param>
        /// <param name="clientId">当前的 clientId</param>
        /// <param name="targetIds">被操作所影响到的 clientIds</param>
        /// <param name="action">执行的操作，目前只有 add，remove</param>
        /// <returns></returns>
        public Task<AVIMSignatureV2> CreateConversationSignature(string conversationId, string clientId, IList<string> targetIds, string action)
        {
            var data = new Dictionary<string, object>();

            data.Add("client_id", clientId);//表示当前是谁在操作。
            data.Add("member_ids", targetIds);//memberIds不要包含当前的ClientId。
            data.Add("conversation_id", conversationId);//conversationId是签名必须的参数。

            data.Add("action", action);//conversationId是签名必须的参数。


            //调用云代码进行签名。
            return AVCloud.CallFunctionAsync<IDictionary<string, object>>("signActionOnCoversation", data).ContinueWith<AVIMSignatureV2>(t =>
            {
                return MakeSignature(t.Result); ;//拼装成一个 Signature 对象
            });
            //以上这段代码，开发者无需手动调用，只要开发者对一个 AVIMClient 设置了 SignatureFactory，SDK 会在执行对应的操作时主动调用这个方法进行签名。
        }


        /// <summary>
        /// 登陆签名
        /// </summary>
        /// <param name="clientId">当前的 clientId</param>
        /// <returns></returns>
        public Task<AVIMSignatureV2> CreateConnectSignature(string clientId)
        {
            var data = new Dictionary<string, object>();

            data.Add("client_id", clientId);//表示当前是谁要求连接服务器。 

            //调用云代码进行签名。
            return AVCloud.CallFunctionAsync<IDictionary<string, object>>("signConnect", data).ContinueWith<AVIMSignatureV2>(t =>
            {
                return MakeSignature(t.Result); ;//拼装成一个 Signature 对象
            });
        }

        /// <summary>
        /// 为创建对话签名
        /// </summary>
        /// <param name="clientId">当前的 clientId </param>
        /// <param name="targetIds">被影响的 clientIds </param>
        /// <returns></returns>
        public Task<AVIMSignatureV2> CreateStartConversationSignature(string clientId, IList<string> targetIds)
        {
            var data = new Dictionary<string, object>();

            data.Add("client_id", clientId);//表示当前是谁在操作。
            data.Add("member_ids", targetIds);//memberIds不要包含当前的ClientId。

            //调用云代码进行签名。
            return AVCloud.CallFunctionAsync<IDictionary<string, object>>("signStartConversation", data).ContinueWith<AVIMSignatureV2>(t =>
            {
                return MakeSignature(t.Result); ;//拼装成一个 Signature 对象
            });
        }

        /// <summary>
        /// 获取签名信息并且把它返回给 SDK 去进行下一步的操作
        /// </summary>
        /// <param name="dataFromCloudcode"></param>
        /// <returns></returns>
        protected AVIMSignatureV2 MakeSignature(IDictionary<string, object> dataFromCloudcode)
        {
            AVIMSignatureV2 signature = new AVIMSignatureV2();
            signature.Nonce = dataFromCloudcode["nonce"].ToString();
            signature.SignatureContent = dataFromCloudcode["signature"].ToString();
            signature.Timestamp = (long)dataFromCloudcode["timestamp"];
            return signature;//拼装成一个 Signature 对象
        }

        /// <summary>
        /// 为获取聊天记录的操作签名
        /// </summary>
        /// <param name="clientId">当前的 clientId </param>
        /// <param name="conversationId">对话 Id</param>
        /// <returns></returns>
        public Task<AVIMSignatureV2> CreateQueryHistorySignature(string clientId, string conversationId)
        {
            var data = new Dictionary<string, object>();

            data.Add("client_id", clientId);//表示当前是谁在操作。
            data.Add("convid", conversationId);//memberIds不要包含当前的ClientId。

            //调用云代码进行签名。
            return AVCloud.CallFunctionAsync<IDictionary<string, object>>("signQueryHistory", data).ContinueWith<AVIMSignatureV2>(t =>
            {
                return MakeSignature(t.Result); ;//拼装成一个 Signature 对象
            });
        }
    }
}
